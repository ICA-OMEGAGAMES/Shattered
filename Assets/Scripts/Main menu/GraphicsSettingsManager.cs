using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//Manages selection of configurations, storing configurations and changing them
public class GraphicsSettingsManager : MonoBehaviour {
	public Toggle fullscreenToggle;
	
	public Dropdown resolutionDropdown;
	public Dropdown graphicsQualityDropdown;
	public Dropdown textureQualityDropdown;
	public Dropdown antialiasingDropdown;
	public Dropdown vSyncDropdown;
	public Button backButton;

	private Resolution[] resolutions;

	void OnEnable()
	{
		SetUpResolutions();
		SetUpGraphicsQuality();
		
		fullscreenToggle.isOn = GraphicSettings.Instance.fullscreen = Screen.fullScreen;
		textureQualityDropdown.value = GraphicSettings.Instance.textureQuality = QualitySettings.masterTextureLimit;
		GraphicSettings.Instance.antialiasing = QualitySettings.antiAliasing;
		antialiasingDropdown.value = (int)Mathf.Log(GraphicSettings.Instance.antialiasing, 2);
		vSyncDropdown.value = GraphicSettings.Instance.vSync = QualitySettings.vSyncCount;
		graphicsQualityDropdown.value = GraphicSettings.Instance.graphicsQuality = QualitySettings.GetQualityLevel();;
		
		if(graphicsQualityDropdown.value != 0)
		{
			textureQualityDropdown.interactable = false;
			antialiasingDropdown.interactable = false;
			vSyncDropdown.interactable = false;
		}
		
		AddListeners();
	}

	public void OnFullScreenToggle(){
		GraphicSettings.Instance.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
	}

	public void OnGraphicsQualityChange()
	{
		GraphicSettings.Instance.graphicsQuality = graphicsQualityDropdown.value;
		QualitySettings.SetQualityLevel(GraphicSettings.Instance.graphicsQuality, true);

		bool isEnabled = (GraphicSettings.Instance.graphicsQuality ==  0);
		textureQualityDropdown.interactable = isEnabled;
		antialiasingDropdown.interactable = isEnabled;
		vSyncDropdown.interactable = isEnabled;
		
		SyncValues();
	}

	public void OnResolutionChange()
	{
		Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
		GraphicSettings.Instance.resolutionIndex = resolutionDropdown.value;
	}

	public void OnTextureQualityChange()
	{
		GraphicSettings.Instance.textureQuality = QualitySettings.masterTextureLimit = textureQualityDropdown.value;
	}

	public void OnAntialiasingChange()
	{
		QualitySettings.antiAliasing = GraphicSettings.Instance.antialiasing = (int)Mathf.Pow(2, antialiasingDropdown.value);
	}

	public void OnVsyncChange()
	{
		GraphicSettings.Instance.vSync = QualitySettings.vSyncCount = vSyncDropdown.value;
	}

	private void AddListeners()
	{
		fullscreenToggle.onValueChanged.AddListener(delegate {OnFullScreenToggle();});
		graphicsQualityDropdown.onValueChanged.AddListener(delegate {OnGraphicsQualityChange();});
		resolutionDropdown.onValueChanged.AddListener(delegate {OnResolutionChange();});
		textureQualityDropdown.onValueChanged.AddListener(delegate {OnTextureQualityChange();});
		antialiasingDropdown.onValueChanged.AddListener(delegate {OnAntialiasingChange();});
		vSyncDropdown.onValueChanged.AddListener(delegate {OnVsyncChange();});
		backButton.onClick.AddListener(delegate{SaveSettings();});
	}

	private void SyncValues()
	{
		GraphicSettings.Instance.fullscreen = fullscreenToggle.isOn = Screen.fullScreen;
		GraphicSettings.Instance.textureQuality = textureQualityDropdown.value = QualitySettings.masterTextureLimit;
		GraphicSettings.Instance.antialiasing = QualitySettings.antiAliasing;
		antialiasingDropdown.value = (int)Mathf.Log(GraphicSettings.Instance.antialiasing, 2);
		GraphicSettings.Instance.vSync = vSyncDropdown.value = QualitySettings.vSyncCount;
		GraphicSettings.Instance.graphicsQuality = graphicsQualityDropdown.value = QualitySettings.GetQualityLevel();
	}

	private void SetUpResolutions()
	{
		int counter = 0;
		resolutions = Screen.resolutions;
		resolutionDropdown.options.RemoveRange(0, resolutionDropdown.options.Count);
		foreach(Resolution reso in resolutions)
		{
			resolutionDropdown.options.Add(new Dropdown.OptionData(reso.width + " x " + reso.height + " @" + reso.refreshRate + "Hz"));
			if(reso.height.Equals(Screen.height) && reso.width.Equals(Screen.width))
			{
				GraphicSettings.Instance.resolutionIndex = resolutionDropdown.value = counter;
			}
			counter++;
		}
	}

	private void SetUpGraphicsQuality()
	{
		string[] names = QualitySettings.names;
		graphicsQualityDropdown.options.RemoveRange(0, graphicsQualityDropdown.options.Count);
		foreach(string name in names)
		{
			graphicsQualityDropdown.options.Add(new Dropdown.OptionData(name));
		}
	}

	//Saves current settings to a config file
	public void SaveSettings()
	{
		string jsonData = JsonUtility.ToJson (GraphicSettings.Instance, true);
  		File.WriteAllText (Application.persistentDataPath + "/graphicsettings.json", jsonData);
	}

}




