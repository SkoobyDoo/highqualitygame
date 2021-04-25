using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject m_camera;
	public List<GameObject> m_cameraFollowTargets = new List<GameObject>();
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 tempSum = new Vector3();
		foreach (var x in m_cameraFollowTargets)
		{
			tempSum += x.transform.position;
		}
		tempSum /= m_cameraFollowTargets.Count;
		
		
        m_camera.transform.position = new Vector3(tempSum.x, tempSum.y, m_camera.transform.position.z);
    }
	
}
