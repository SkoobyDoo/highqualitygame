using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDangler2D : MonoBehaviour
{
	public GameObject m_RopeDefinition;
	public Rigidbody2D m_RopeConnectionBody;
	public int m_numSegments = 10;
	public Transform m_ropeAnchor;
	
	private GameObject lastRopeSegment;
	
    // Start is called before the first frame update
    void Start()
    {
		for(int i = 0; i < m_numSegments; i++) {
			AddRopeSegment();
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void AddRopeSegment()
	{
		GameObject newSegment = Instantiate(m_RopeDefinition, new Vector3(0,0,0), Quaternion.identity);
		if(lastRopeSegment == null) {
			lastRopeSegment = newSegment;
			// connected rigid body for joint to helicopter rigid body
			HingeJoint2D ropeJoint = lastRopeSegment.GetComponent<HingeJoint2D>();
			ropeJoint.connectedBody = m_RopeConnectionBody;
			FixedJoint2D ropeSecondJoint = lastRopeSegment.GetComponent<FixedJoint2D>();
			ropeSecondJoint.connectedBody = m_RopeConnectionBody;
			// set connected anchor x,Y coord to the ropeAnchor transform x,y values
			ropeJoint.connectedAnchor = new Vector2(m_ropeAnchor.transform.localPosition.x, m_ropeAnchor.transform.localPosition.y);
			ropeSecondJoint.connectedAnchor = new Vector2(m_ropeAnchor.transform.localPosition.x, m_ropeAnchor.transform.localPosition.y);
			// reposition new segment to be near the anchor location
			lastRopeSegment.transform.SetParent(m_ropeAnchor.transform);
			lastRopeSegment.transform.position = m_ropeAnchor.transform.position;
		}
		else {
			// connected rigid body for joint to previous segment rigid body
			HingeJoint2D ropeJoint = newSegment.GetComponent<HingeJoint2D>();
			Rigidbody2D lastBody = lastRopeSegment.GetComponent<Rigidbody2D>();
			ropeJoint.connectedBody = lastBody;
			FixedJoint2D ropeSecondJoint = newSegment.GetComponent<FixedJoint2D>();
			ropeSecondJoint.connectedBody = lastBody;
			// reposition new segment to be near the tip of the last one
			newSegment.transform.position = lastRopeSegment.transform.position;
			float length = newSegment.GetComponent<CapsuleCollider2D>().size.y;
			newSegment.transform.position = new Vector2(newSegment.transform.position.x, newSegment.transform.position.y - length);
			// ---
			newSegment.transform.SetParent(lastRopeSegment.transform);
			lastRopeSegment = newSegment;
		}
	}
}
