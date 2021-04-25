using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteBoomer : MonoBehaviour
{
	public float m_duration = 5f;
	
	private float m_age = 0f;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate()
    {
        m_age += Time.fixedDeltaTime;
		if( m_age > m_duration ) {
			// TODO: bewm?
			Destroy(gameObject);
		}
    }
}
