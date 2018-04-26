using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//Controls for sound settings
public class SoundSettingsManager : MonoBehaviour {
	public Slider masterVolumeSlider;
	public Slider musicVolumeSlider;
	public Slider soundEffectsVolumeSlider;
	public Button backButton;

	public AudioSource musicSource;
	public AudioSource soundEffectsSource;

	void OnEnable()
	{
		masterVolumeSlider.value = AudioListener.volume = SoundSettings.Instance.masterVolume;
		musicSource.volume = musicVolumeSlider.value = SoundSettings.Instance.musicVolume;
		soundEffectsSource.volume = soundEffectsVolumeSlider.value = SoundSettings.Instance.soundEffectsVolume;

		masterVolumeSlider.onValueChanged.AddListener(delegate {OnMasterVolumeChange();});
		musicVolumeSlider.onValueChanged.AddListener(delegate {OnMusicVolumeChange();});
		soundEffectsVolumeSlider.onValueChanged.AddListener(delegate {OnSoundEffectVolumeChange();});
		backButton.onClick.AddListener(delegate{SaveSettings();});
	}

	public void OnMasterVolumeChange(){
		SoundSettings.Instance.masterVolume = AudioListener.volume = masterVolumeSlider.value;
	}

	public void OnMusicVolumeChange()
	{
		musicSource.volume = SoundSettings.Instance.musicVolume = musicVolumeSlider.value;
	}

	public void OnSoundEffectVolumeChange()
	{
		soundEffectsSource.volume = SoundSettings.Instance.soundEffectsVolume = soundEffectsVolumeSlider.value;
	}

	public void SaveSettings()
	{
		string jsonData = JsonUtility.ToJson (SoundSettings.Instance, true);
  		File.WriteAllText (Application.persistentDataPath + "/soundsettings.json", jsonData);
	}
}


