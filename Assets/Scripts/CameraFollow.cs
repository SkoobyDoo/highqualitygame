using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject m_camera;
	public List<GameObject> m_cameraFollowTargets = new List<GameObject>();
	public float m_minCameraSize = 5f;
	public float m_maxCameraSize = 50f;
	public float m_minSpeed = 1f;
	public float m_maxSpeed = 30f;
	[Range(0, .5f)] public float m_cameraZoomSmoothing = .3f;
	
	private float camVelocity = 0f;
	
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
		
		//float speedRatio = (Mathf.Clamp(m_cameraFollowTargets[0].GetComponent<Rigidbody2D>().velocity, m_minSpeed, m_maxSpeed) - m_minSpeed) / (m_maxSpeed - m_minSpeed);
		float speedRatio = Mathf.InverseLerp(m_minSpeed,m_maxSpeed,m_cameraFollowTargets[0].GetComponent<Rigidbody2D>().velocity.magnitude);
		float targetZoom = Mathf.Lerp(m_minCameraSize,m_maxCameraSize,speedRatio);
		Camera cam = m_camera.GetComponent<Camera>();
		cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref camVelocity, m_cameraZoomSmoothing);
    }
	
}
