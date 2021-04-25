using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject m_camera;
	public Gradient m_backgroundGradient; // gradient for color between min and max depth
	public float m_minDepth = 0f; // depth to use first gradient color above
	public float m_maxDepth = -100f; // depth to use second gradient color below
	public List<GameObject> m_cameraFollowTargets = new List<GameObject>();
	[Range(0, .5f)] public float m_cameraPosSmoothing = .5f;
	public float m_minCameraSize = 5f;
	public float m_maxCameraSize = 50f;
	public float m_minSpeed = 1f;
	public float m_maxSpeed = 30f;
	[Range(0, .5f)] public float m_cameraZoomSmoothing = .3f;
	
	private Vector3 camVelocity = new Vector3();
	private float camZoomVelocity = 0f;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 targetPos = new Vector3();
		foreach (var x in m_cameraFollowTargets)
		{
			targetPos += x.transform.position;
		}
		targetPos /= m_cameraFollowTargets.Count;
		targetPos.z = m_camera.transform.position.z;
		
		Vector3 currentPos = m_camera.transform.position;
        m_camera.transform.position = Vector3.SmoothDamp(currentPos, targetPos, ref camVelocity, m_cameraPosSmoothing);
		
		//float speedRatio = (Mathf.Clamp(m_cameraFollowTargets[0].GetComponent<Rigidbody2D>().velocity, m_minSpeed, m_maxSpeed) - m_minSpeed) / (m_maxSpeed - m_minSpeed);
		float speedRatio = Mathf.InverseLerp(m_minSpeed,m_maxSpeed,m_cameraFollowTargets[0].GetComponent<Rigidbody2D>().velocity.magnitude);
		float targetZoom = Mathf.Lerp(m_minCameraSize,m_maxCameraSize,speedRatio);
		Camera cam = m_camera.GetComponent<Camera>();
		cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref camZoomVelocity, m_cameraZoomSmoothing);
		
		//cameraGradient
		float depthRatio = ( Mathf.Clamp(m_cameraFollowTargets[0].transform.position.y, m_maxDepth, m_minDepth) - m_minDepth ) / (m_maxDepth - m_minDepth);
		cam.backgroundColor = m_backgroundGradient.Evaluate(depthRatio);
    }
	
}
