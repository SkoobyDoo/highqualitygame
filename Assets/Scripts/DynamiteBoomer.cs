using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteBoomer : MonoBehaviour
{
	public float m_duration = 5f;
	
	private float m_age = 0f;
    public float explosionRadius = 2.5f;
	
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
            ExplosionDamage(gameObject.transform.position, explosionRadius);
		}
    }

    void ExplosionDamage(Vector2 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            {
                hitCollider.SendMessage("AddDamage");
            }
        }
    }
}
