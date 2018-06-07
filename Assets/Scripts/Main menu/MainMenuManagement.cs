using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

// Manages setup and configuration of main menu itself
public class MainMenuManagement : MonoBehaviour {

	private string noSettingsFoundMessage = " - No saved settings found.";
	public PlayVideo backgroundVideo;

	void Start () {
		LoadGraphicsSettings();
		LoadSoundSettings();
		LoadControlSettings();
		// backgroundVideo.startVideo();
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

	void LoadControlSettings(){
		string[] json = null;
		try
		{
			json = File.ReadAllLines(Application.persistentDataPath + "/controlSettings.json");
		} 
		catch(FileNotFoundException e)
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
			key = Regex.Replace(key,  "[\"{} ]", "");
			
			if(key.Length < 1)
			{
				continue;
			}
			
			if(key.Contains("[") && key.Contains("]"))
			{
				key = Regex.Replace(key, "[][]", "");
				if(key.EndsWith(","))
				{
					key = key.Substring(0, key.Length - 1);
				}

				string name = key.Split(':')[0];
				key = key.Replace(name + ":", "");

				ControlAxis axis = new ControlAxis();
				axis.axisName = name;
				axis.positiveKey = key.Split(',')[0];
				axis.negativeKey =  key.Split(',')[1];
				ControlSettings.Instance.AddAxis(axis);
				
				continue;
			}

			key = key.Replace(",", "");

			ControlButton button = new ControlButton();
			button.buttonName = key.Split(':')[0];
			button.assignedKey = key.Split(':')[1];
			ControlSettings.Instance.AddButton(button);
		}
	}
}
