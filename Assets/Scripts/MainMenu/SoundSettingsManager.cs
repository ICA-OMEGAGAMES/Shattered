using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controls for sound settings
public class SoundSettingsManager : MonoBehaviour {
	public Slider masterVolumeSlider;
	public Slider musicVolumeSlider;
	public Slider soundEffectsVolumeSlider;
	
	public SoundSettings soundSettings;

	void OnEnable()
	{
		soundSettings = new SoundSettings();

		masterVolumeSlider.value = AudioListener.volume;

		masterVolumeSlider.onValueChanged.AddListener(delegate {OnMasterVolumeChange();});
		musicVolumeSlider.onValueChanged.AddListener(delegate {OnMusicVolumeChange();});
		soundEffectsVolumeSlider.onValueChanged.AddListener(delegate {OnSoundEffectVolumeChange();});
	}

	public void OnMasterVolumeChange(){
		soundSettings.masterVolume = AudioListener.volume = masterVolumeSlider.value;
	}

	public void OnMusicVolumeChange()
	{
		//TODO
	}

	public void OnSoundEffectVolumeChange()
	{
		//TODO
	}

	public void SaveSettings()
	{

	}

	public void LoadSettings()
	{

	}
}


