using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMainArea : MonoBehaviour
{
    public AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D other)
    { // https://gamedevbeginner.com/how-to-play-audio-in-unity-with-examples/#:~:text=Unity%20has%20a%20built%20in,of%20the%20drop%20down%20menu.
        if (other.tag == "Player" && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
