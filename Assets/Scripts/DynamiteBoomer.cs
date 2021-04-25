using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteBoomer : MonoBehaviour
{
	public GameObject m_particle;
	public float m_duration = 5f;
	
	private float m_age = 0f;
    public float explosionRadius = 25.0f;
	
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
			Instantiate(m_particle,transform.position, Quaternion.identity);
            ExplosionDamage(gameObject.transform.position, explosionRadius);
            Destroy(gameObject);
        }
    }

    void ExplosionDamage(Vector2 center, float radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            {
                Debug.Log("found a damageable");
                hitCollider.SendMessage("AddDamage");
            }
        }
    }
}
