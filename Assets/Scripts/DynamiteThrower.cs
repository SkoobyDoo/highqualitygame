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
	public AudioClip[] ThrowClips;
	public AudioClip[] NoAmmoClips;
	public AudioClip[] AmmoPickupClips;
	public AudioClip dedAudio;
	public AudioClip wilhelmScream;
	public int currentDynomiteCount = 5;
	public bool disableThrow = false;

	AudioClip RandomThrowClip()
    {
		return ThrowClips[UnityEngine.Random.Range(0, ThrowClips.Length)];
    }

	// Start is called before the first frame update
	void Start()
    {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		DurationThrowPrepTime = (long)(10000000 * DurationThrowPrepTimeSeconds);
		disableThrow = false;
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

		if (Input.GetKeyDown(KeyCode.R) && !disableThrow)
		{
			Debug.Log(disableThrow);
			dudeIsAlive = !dudeIsAlive;
			ChangeSprite();
		}

		if (Input.GetButton("Fire1") && !disableThrow)
		{
			if (currentDynomiteCount == 0 && !audioSource.isPlaying)
            {
				audioSource.PlayOneShot(RandomNoAmmoClip());
            }

			else if (throwState == "Unprepped" && !disableThrow)
			{ 
				StartThrowPrepTime = DateTime.Now.Ticks;
				TargetThrowPrepTime = DurationThrowPrepTime;
				throwState = "PrepThrow";
			}

			else if (throwState == "PrepThrow" && !disableThrow)
			{
				CurrentThrowPrepTime = DateTime.Now.Ticks - StartThrowPrepTime;
				CurrentSquashPercent = SinSquashToTargetVal(CurrentThrowPrepTime, TargetThrowPrepTime);
			}
		}

		if (Input.GetButtonUp("Fire1") && throwState == "PrepThrow" && currentDynomiteCount >= 1 && !disableThrow) {
			throwState = "Unprepped";
			currentDynomiteCount -= 1;
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Vector2 direction = (Vector2)((mousePos - transform.position)); 
			direction.Normalize();
			float throwLength = CurrentSquashPercent * maxThrowMagnitude;
			
			GameObject dynamite = Instantiate(projectile, transform.position, Quaternion.identity);
			Rigidbody2D dynamiteBody = dynamite.GetComponent<Rigidbody2D>();
			dynamiteBody.velocity = direction * throwLength;
			// Debug.Log(CurrentSquashPercent);
			// Debug.Log(maxThrowMagnitude);
			// Debug.Log(throwLength);
			dynamiteBody.AddTorque(UnityEngine.Random.Range(2f,-2f));
			audioSource.PlayOneShot(RandomThrowClip());
		}
	}

	void AddDamage()
	{
		dudeIsAlive = false;
		audioSource.PlayOneShot(dedAudio);
	}
	void AddDynomite()
	{
		currentDynomiteCount += 3;
		audioSource.PlayOneShot(RandomAmmoPickupClip());
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

	AudioClip RandomNoAmmoClip()
	{
		return NoAmmoClips[UnityEngine.Random.Range(0, NoAmmoClips.Length)];
	}
	AudioClip RandomAmmoPickupClip()
	{
		return AmmoPickupClips[UnityEngine.Random.Range(0, AmmoPickupClips.Length)];
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
