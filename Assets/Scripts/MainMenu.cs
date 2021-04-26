using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
	[Header("Main Menu")]
	public GameObject MainMenuObject;
	public TextMeshProUGUI VersionLabel;

	[Header("Settings Menu")]
	public GameObject SettingsObject;
	public Toggle[] ResolutionToggles;
	public AudioMixer MainMixer;
	public TextMeshProUGUI MasterVolumeLabel;
	public Slider MasterVolumeSlider;
	public TextMeshProUGUI SFXVolumeLabel;
	public Slider SFXVolumeSlider;
	public TextMeshProUGUI MusicVolumeLabel;
	public Slider MusicVolumeSlider;
	public TextMeshProUGUI DialogueVolumeLabel;
	public Slider DialogueVolumeSlider;

	private void Start()
    {
		MainMenuObject.SetActive(true);
		SettingsObject.SetActive(false);

		//Main Menu
		if(VersionLabel != null)
			VersionLabel.text = Application.version;

		//Settings Menu
		InitializeResolutionToggles();

		MasterVolumeLabel.text = MasterVolumeSlider.value.ToString("P0");
	}

	//Main Menu
    public void PlayScene(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}

	public void ToSettings()
	{
		MainMenuObject.SetActive(false);
		SettingsObject.SetActive(true);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void GoToWebPage(string url)
	{
		Application.OpenURL(url);
	}

	//Settings

	//Set the resolution toggle to the current resolution
	private void InitializeResolutionToggles()
	{
		float resolutionRatio = Screen.height / 1080f;
		int resolutionIndex = 0;

		if (resolutionRatio < 0.99f)
			resolutionIndex = 0;
		else if (resolutionRatio > 0.99f && resolutionRatio < 1.01f)
			resolutionIndex = 1;
		else if (resolutionRatio > 1.01 && resolutionRatio < 1.99f)
			resolutionIndex = 2;
		else if (resolutionRatio > 1.99f)
			resolutionIndex = 3;

		ResolutionToggles[0].SetIsOnWithoutNotify(resolutionIndex == 0);
		ResolutionToggles[1].SetIsOnWithoutNotify(resolutionIndex == 1);
		ResolutionToggles[2].SetIsOnWithoutNotify(resolutionIndex == 2);
		ResolutionToggles[3].SetIsOnWithoutNotify(resolutionIndex == 3);
	}

	public void ToMainMenu()
	{
		MainMenuObject.SetActive(true);
		SettingsObject.SetActive(false);
	}

	public void ToggleFullscreen(bool fullscreen)
	{
		Screen.fullScreen = fullscreen;
	}

	public void SelectResolution(int id)
	{
		switch(id)
		{
			case 0:
				Screen.SetResolution(1280, 720, Screen.fullScreen); break;
			default: case 1:
				Screen.SetResolution(1920, 1080, Screen.fullScreen); break;
			case 2:
				Screen.SetResolution(2560, 1440, Screen.fullScreen); break;
			case 3:
				Screen.SetResolution(3840, 2160, Screen.fullScreen); break;
		}

		ResolutionToggles[0].SetIsOnWithoutNotify(id == 0);
		ResolutionToggles[1].SetIsOnWithoutNotify(id == 1);
		ResolutionToggles[2].SetIsOnWithoutNotify(id == 2);
		ResolutionToggles[3].SetIsOnWithoutNotify(id == 3);
	}

	public void SetMasterVolume(float value)
	{
		MasterVolumeLabel.text = value.ToString("P0");

		MainMixer.SetFloat("volumeMaster", Mathf.LerpUnclamped(-50f, 0f, value));
	}

	public void SetSFXVolume(float value)
	{
		SFXVolumeLabel.text = value.ToString("P0");

		MainMixer.SetFloat("volumeSFX", Mathf.LerpUnclamped(-50f, 0f, value));
	}

	public void SetMusicVolume(float value)
	{
		MusicVolumeLabel.text = value.ToString("P0");

		MainMixer.SetFloat("volumeMusic", Mathf.LerpUnclamped(-50f, 0f, value));
	}

	public void SetDialogueVolume(float value)
	{
		DialogueVolumeLabel.text = value.ToString("P0");

		MainMixer.SetFloat("volumeDialogue", Mathf.LerpUnclamped(-50f, 0f, value));
	}
}
