using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip explosionClip;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(explosionClip);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!audioSource.isPlaying)
        //{
        //    // Debug.Log("should be playing boom here");
        //}
    }
}
