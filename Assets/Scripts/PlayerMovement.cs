using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController2D controller;
    public AudioSource audioSource;
	

	float horizontalMove = 0f;
	float verticalMove = 0f;
    public bool disableStuff;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        disableStuff = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        if (new Vector2(horizontalMove, verticalMove).magnitude > 0 && !audioSource.isPlaying && !disableStuff)
        {
            audioSource.Play();
        }
        else if (new Vector2(horizontalMove, verticalMove).magnitude == 0 && audioSource.isPlaying && !disableStuff)
        {
            audioSource.Stop();
        }
    }
	
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
    }
}
