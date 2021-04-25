using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject PauseMenuObject;

    void Start()
    {
		ShowMenu(false);
	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
			ShowMenu(!PauseMenuObject.activeSelf);
    }

	private void ShowMenu(bool b)
	{
		if (PauseMenuObject.activeSelf == b)
			return;

		PauseMenuObject.SetActive(b);
		Time.timeScale = b ? 0f : 1f;
	}

	public void Resume()
	{
		ShowMenu(false);
	}

	public void ToMenu(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
