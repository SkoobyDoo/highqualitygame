using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject PauseMenuObject;
	public GameObject playerObject;
	DynamiteThrower playerScript;

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
		playerScript.disableThrow = b;
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
