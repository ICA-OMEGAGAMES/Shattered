using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Manages setup and configuration of main menu itself
public class MainMenuManagement : MonoBehaviour {

	private string noSettingsFoundMessage = " - No saved settings found.";

	void Start () {
		LoadGraphicsSettings();
		LoadSoundSettings();
	}

	//Loads and applies stored settings from the config file	
	void LoadGraphicsSettings(){
		string json = null;
		try
		{
			json = File.ReadAllText(Application.persistentDataPath + Constants.GRAPHICS_JSON);		
		} catch(FileNotFoundException e)
		{
			Debug.Log(e.Message + noSettingsFoundMessage);
		}
		
		
		if(json == null)
		{
			return;
		}

		GraphicSettings.Instance.graphicsQuality = JsonUtility.FromJson<GraphicSettings>(json).graphicsQuality;
		QualitySettings.SetQualityLevel(GraphicSettings.Instance.graphicsQuality);
		
		int resolutionIndex = GraphicSettings.Instance.resolutionIndex = JsonUtility.FromJson<GraphicSettings>(json).resolutionIndex;
		if(resolutionIndex < Screen.resolutions.Length)
		{
			Screen.SetResolution(Screen.resolutions[resolutionIndex].width, Screen.resolutions[resolutionIndex].height, Screen.fullScreen);
		}

		Screen.fullScreen = GraphicSettings.Instance.fullscreen = JsonUtility.FromJson<GraphicSettings>(json).fullscreen;
		QualitySettings.antiAliasing = GraphicSettings.Instance.antialiasing = JsonUtility.FromJson<GraphicSettings>(json).antialiasing;
		QualitySettings.masterTextureLimit = GraphicSettings.Instance.textureQuality = JsonUtility.FromJson<GraphicSettings>(json).textureQuality;
		QualitySettings.vSyncCount = GraphicSettings.Instance.vSync = JsonUtility.FromJson<GraphicSettings>(json).vSync;
	}

	void LoadSoundSettings(){
		string json = null;
		try
		{
			json = File.ReadAllText(Application.persistentDataPath + Constants.SOUND_JSON);
		} catch(FileNotFoundException e)
		{
			Debug.Log(e.Message + noSettingsFoundMessage);
		}
		
		if(json == null)
		{
			return;
		}

		SoundSettings.Instance.masterVolume = JsonUtility.FromJson<SoundSettings>(json).masterVolume;
		SoundSettings.Instance.musicVolume = JsonUtility.FromJson<SoundSettings>(json).musicVolume;
		SoundSettings.Instance.soundEffectsVolume = JsonUtility.FromJson<SoundSettings>(json).soundEffectsVolume;
	}
}
