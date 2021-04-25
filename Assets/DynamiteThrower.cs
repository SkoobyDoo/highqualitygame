using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteThrower : MonoBehaviour
{
	public GameObject projectile;
	public float m_throwStrength = 6f;
	public float m_throwRatio = 12f;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			Vector2 direction = (Vector2)((mousePos - transform.position));
			float throwLength = direction.magnitude;
			direction.Normalize();
			
			GameObject dynamite = Instantiate(projectile, transform.position, Quaternion.identity);
			Rigidbody2D dynamiteBody = dynamite.GetComponent<Rigidbody2D>();
			dynamiteBody.velocity = direction * (m_throwStrength * throwLength / m_throwRatio);
			dynamiteBody.AddTorque(Random.Range(2f,-2f));
		}
    }
}
