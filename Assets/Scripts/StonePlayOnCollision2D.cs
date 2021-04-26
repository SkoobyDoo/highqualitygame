using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePlayOnCollision2D : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] rockHitClips;
    public float maxForce = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float force = collision.relativeVelocity.magnitude;
        float volume = 1;

        if (force <= maxForce)
        {
            volume = force / maxForce;
        }
        audioSource.PlayOneShot(RandomClip(), volume); 
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    AudioClip RandomClip()
    {
        return rockHitClips[UnityEngine.Random.Range(0, rockHitClips.Length)];
    }
}
