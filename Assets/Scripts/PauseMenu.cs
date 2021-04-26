using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject PauseMenuObject;
	public GameObject playerObject;
	DynamiteThrower playerScript;
	public GameObject heliObject;
	PlayerMovement heliScript;

	void Start()
    {
		ShowMenu(false);
	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			ShowMenu(!PauseMenuObject.activeSelf);
		}
    }

	private void ShowMenu(bool b)
	{
		if (PauseMenuObject.activeSelf == b)
			return;

		// Debug.Log(b);
		PauseMenuObject.SetActive(b);
		playerObject = GameObject.FindGameObjectWithTag("DynomiteDude");
		playerScript = playerObject.GetComponent<DynamiteThrower>();
		heliObject = GameObject.FindGameObjectWithTag("Player");
		heliScript = heliObject.GetComponent<PlayerMovement>();
		playerScript.disableThrow = b;
		heliScript.disableStuff = b;
		Time.timeScale = b ? 0f : 1f;
	}

	public void Resume()
	{
		ShowMenu(false);
	}

	public void ToMenu(int sceneIndex)
	{
		ShowMenu(false);
		SceneManager.LoadScene(sceneIndex);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
