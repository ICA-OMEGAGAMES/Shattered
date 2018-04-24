using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Manages setup and configuration of main menu itself
public class MainMenuManagement : MonoBehaviour {

	void Start () {
		LoadGraphicsSettings();
		LoadSoundSettings();
		LoadControlSettings();
	}

	//Loads and applies stored settings from the config file	
	void LoadGraphicsSettings(){
		string json = null;
		try
		{
			json = File.ReadAllText(Application.persistentDataPath + "/graphicsettings.json");		
		} catch(FileNotFoundException e)
		{
			Debug.Log(e.Message + " - No saved settings found.");
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
			json = File.ReadAllText(Application.persistentDataPath + "/soundsettings.json");
		} catch(FileNotFoundException e)
		{
			Debug.Log(e.Message + " - No saved settings found.");
		}
		
		if(json == null)
		{
			return;
		}

		SoundSettings.Instance.masterVolume = JsonUtility.FromJson<SoundSettings>(json).masterVolume;
		SoundSettings.Instance.musicVolume = JsonUtility.FromJson<SoundSettings>(json).musicVolume;
		SoundSettings.Instance.soundEffectsVolume = JsonUtility.FromJson<SoundSettings>(json).soundEffectsVolume;
	}

	void LoadControlSettings(){
		string[] json = null;
		try
		{
			json = File.ReadAllLines(Application.persistentDataPath + "/controlSettings.json");
		} catch(FileNotFoundException e)
		{
			Debug.Log(e.Message + " - No saved settings found.");
		}
		
		if(json == null)
		{
			return;
		}
		foreach(string setting in json)
		{
			string key = setting;
			key = key.Replace("\"", "");
			key = key.Replace(" ", "");
			key = key.Replace("{", "");
			key = key.Replace("}", "");
			key = key.Replace(",", "");

			if(key.Length < 1)
			{
				continue;
			}
			
			ControlsSettings.Instance.Add(new KeyValuePair<string, KeyCode>(key.Split(':')[0], (KeyCode)System.Enum.Parse(typeof(KeyCode), key.Split(':')[1], true)));
		}
	}
}
