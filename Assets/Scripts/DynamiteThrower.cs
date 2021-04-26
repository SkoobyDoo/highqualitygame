using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DynamiteThrower : MonoBehaviour
{
	public GameObject projectile;
	public SpriteRenderer spriteRenderer;
	public Sprite dedSprite;
	public Sprite aliveSprite;
	public float m_throwStrength = 6f;
	public float m_throwRatio = 12f;
	public bool dudeIsAlive = true;
	private string throwState = "Unprepped";
	private long StartThrowPrepTime;
	private long CurrentThrowPrepTime;
	private long TargetThrowPrepTime;
	public float DurationThrowPrepTimeSeconds = 3.0f;
	private long DurationThrowPrepTime;
	private float ResultVal;
	private float CurrentSquashPercent;
	public float maxThrowMagnitude = 20.0f;
	public AudioSource audioSource;
	public AudioClip[] audioClipArray;
	public AudioClip dedAudio;
	public AudioClip wilhelmScream;

	AudioClip RandomClip()
    {
		return audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)];
    }

	// Start is called before the first frame update
	void Start()
    {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		DurationThrowPrepTime = (long)(10000000 * DurationThrowPrepTimeSeconds);
	}

    // Update is called once per frame
    void Update()
	{
		if (gameObject.GetComponent<Rigidbody2D>().velocity.y < -25 && !audioSource.isPlaying)
        {
			audioSource.PlayOneShot(wilhelmScream, 0.25f);
        }

		if (!dudeIsAlive)
		{
			ChangeSprite();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			dudeIsAlive = !dudeIsAlive;
			ChangeSprite();
		}

		if (Input.GetButton("Fire1"))
		{
			if (throwState == "Unprepped")
			{ 
				StartThrowPrepTime = DateTime.Now.Ticks;
				TargetThrowPrepTime = DurationThrowPrepTime;
				throwState = "PrepThrow";
			}

			if (throwState == "PrepThrow")
			{
				CurrentThrowPrepTime = DateTime.Now.Ticks - StartThrowPrepTime;
				CurrentSquashPercent = SinSquashToTargetVal(CurrentThrowPrepTime, TargetThrowPrepTime);
			}
		}

		if (Input.GetButtonUp("Fire1")) {
			throwState = "Unprepped";
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Vector2 direction = (Vector2)((mousePos - transform.position)); 
			direction.Normalize();
			float throwLength = CurrentSquashPercent * maxThrowMagnitude;
			
			GameObject dynamite = Instantiate(projectile, transform.position, Quaternion.identity);
			Rigidbody2D dynamiteBody = dynamite.GetComponent<Rigidbody2D>();
			dynamiteBody.velocity = direction * throwLength;
			Debug.Log(CurrentSquashPercent);
			Debug.Log(maxThrowMagnitude);
			Debug.Log(throwLength);
			dynamiteBody.AddTorque(UnityEngine.Random.Range(2f,-2f));
			audioSource.PlayOneShot(RandomClip());
		}
	}

	void AddDamage()
	{
		dudeIsAlive = false;
		audioSource.PlayOneShot(dedAudio);
	}

	private float SinSquashToTargetVal(long CurrentTime, long TargetTime)
	{
		if (CurrentTime >= TargetTime)
		{
			return 1.0f;
		}
		ResultVal = (float)(-1.0 * Math.Cos((float)CurrentTime / (float)TargetTime * Math.PI) / 2.0 + 0.5);

		return ResultVal;
	}

	void ChangeSprite()
	{
		if (dudeIsAlive)
		{
			spriteRenderer.sprite = aliveSprite;
		}

		else if (!dudeIsAlive)
		{
			spriteRenderer.sprite = dedSprite;
		}
    }
}
