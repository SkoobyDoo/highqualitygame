using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDangler2D : MonoBehaviour
{
	public GameObject m_RopeDefinition;
	public Rigidbody2D m_RopeConnectionBody;
	public float m_ropeLength = 5f;
	public Transform m_ropeAnchor;
	
	public float m_segmentWidth = 0.05f;
	public float m_segmentLength = 1.5f;
	public float m_segmentGap = 0.005f;
	public float m_segmentMass = 0.005f;
	public float m_segmentGravity = 1f;
	public float m_jointBreakForce = 2f;
	
	public GameObject m_finalObject; // object to stick on the end of the rope
	
	private GameObject lastRopeSegment;
	
    // Start is called before the first frame update
    void Start()
    {
		for(float i = 0; i < m_ropeLength; i+=m_segmentLength) {
			AddRopeSegment();
		}
		
		GameObject finaldude = Instantiate(m_finalObject, new Vector3(0,0,0), Quaternion.identity);
		FixedJoint2D newFixedJoint = finaldude.GetComponent<FixedJoint2D>();
		newFixedJoint.anchor = new Vector2(0f, m_segmentLength/2f);
		newFixedJoint.connectedAnchor = new Vector2(0f, -m_segmentLength/2f);
		Rigidbody2D lastBody = lastRopeSegment.GetComponent<Rigidbody2D>();
		newFixedJoint.connectedBody = lastBody;
		
		HingeJoint2D newHingeJoint = finaldude.GetComponent<HingeJoint2D>();
		newHingeJoint.anchor = new Vector2(0f, m_segmentLength/2f);
		newHingeJoint.connectedAnchor = new Vector2(0f, -m_segmentLength/2f);
		newHingeJoint.connectedBody = lastBody;
		
		finaldude.transform.position = lastRopeSegment.transform.position;
		finaldude.transform.position = new Vector2(finaldude.transform.localPosition.x, finaldude.transform.localPosition.y - m_segmentLength - m_segmentGap);
		finaldude.transform.SetParent(lastRopeSegment.transform);
		
		CameraFollow camScript = GetComponent<CameraFollow>();
		camScript.m_cameraFollowTargets.Add(finaldude);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void AddRopeSegment()
	{
		GameObject newSegment 			= Instantiate(m_RopeDefinition, new Vector3(0,0,0), Quaternion.identity);
		CapsuleCollider2D newCapsule 	= newSegment.GetComponent<CapsuleCollider2D>();
		Rigidbody2D newBody 			= newSegment.GetComponent<Rigidbody2D>();
		HingeJoint2D newHingeJoint 		= newSegment.GetComponent<HingeJoint2D>();
		FixedJoint2D newFixedJoint 		= newSegment.GetComponent<FixedJoint2D>();
		DistanceJoint2D newDistJoint 	= newSegment.GetComponent<DistanceJoint2D>();
		SpriteRenderer newRenderer 		= newSegment.GetComponent<SpriteRenderer>();
		
		// Set constants from parameters
		newCapsule.size = new Vector2(m_segmentWidth, m_segmentLength);
		newBody.mass = m_segmentMass;
		newBody.gravityScale = m_segmentGravity;
		newRenderer.size = new Vector2(m_segmentWidth, m_segmentLength + m_segmentGap);
		newHingeJoint.anchor = new Vector2(0f, m_segmentLength/2f);
		newHingeJoint.connectedAnchor = new Vector2(0f, -m_segmentLength/2f);
		newHingeJoint.breakForce = m_jointBreakForce;
		newFixedJoint.anchor = new Vector2(0f, m_segmentLength/2f);
		newFixedJoint.connectedAnchor = new Vector2(0f, -m_segmentLength/2f);
		newFixedJoint.breakForce = m_jointBreakForce;
		newDistJoint.anchor = new Vector2(0f, m_segmentLength/2f);
		newDistJoint.connectedAnchor = new Vector2(0f, -m_segmentLength/2f);
		newDistJoint.breakForce = m_jointBreakForce;
		newDistJoint.distance = m_segmentGap;
		
		if(lastRopeSegment == null) {
			lastRopeSegment = newSegment;
			// connected rigid body for joint to helicopter rigid body
			newHingeJoint.connectedBody = m_RopeConnectionBody;
			newFixedJoint.connectedBody = m_RopeConnectionBody;
			newDistJoint.connectedBody 	= m_RopeConnectionBody;
			// set connected anchor x,Y coord to the ropeAnchor transform x,y values
			Vector2 specialHeliAnchor = new Vector2(m_ropeAnchor.transform.localPosition.x, m_ropeAnchor.transform.localPosition.y);
			newHingeJoint.connectedAnchor 	= specialHeliAnchor;
			newFixedJoint.connectedAnchor 	= specialHeliAnchor;
			newDistJoint.connectedAnchor 	= specialHeliAnchor;
			// reposition new segment to be near the anchor location
			lastRopeSegment.transform.SetParent(m_ropeAnchor.transform);
			lastRopeSegment.transform.position = m_ropeAnchor.transform.position;
		}
		else {
			// connected rigid body for joint to previous segment rigid body
			Rigidbody2D lastBody = lastRopeSegment.GetComponent<Rigidbody2D>();
			newHingeJoint.connectedBody = lastBody;
			newFixedJoint.connectedBody = lastBody;
			newDistJoint.connectedBody 	= lastBody;
			// reposition new segment to be near the tip of the last one
			newSegment.transform.position = lastRopeSegment.transform.position;
			newSegment.transform.position = new Vector2(newSegment.transform.localPosition.x, newSegment.transform.localPosition.y - m_segmentLength - m_segmentGap);
			// ---
			newSegment.transform.SetParent(lastRopeSegment.transform);
			lastRopeSegment = newSegment;
		}
	}
}
