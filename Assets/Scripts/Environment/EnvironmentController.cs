using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentController : MonoBehaviour {

	public EnvironmentSettings envSettings;
	public Slider corruptionSlider;

	private float corruption = 0;

	// Update is called once per frame
	void Update () {
		corruption = corruptionSlider.value;

		if (corruption != 0) {
			// should make a job for this
			CalculateBloodRainFrequency ();
			CalculateFogDensity ();
			CalculateRainFrequency ();
		} else {
			envSettings.rain.current = 0;
			envSettings.bloodRain.current = 0;
			envSettings.fog.current = 0;
		}
	}

	private void CalculateRainFrequency(){
		if (corruption <= envSettings.rain.maximumCorruption && corruption > envSettings.rain.minimumCorruption)
			envSettings.rain.current  = Mathf.Round(corruption / envSettings.rain.divider) * envSettings.rain.frequency;
		else
			envSettings.rain.current = 0;
	}

	private void CalculateBloodRainFrequency(){
		if (corruption <= envSettings.bloodRain.maximumCorruption && corruption > envSettings.bloodRain.minimumCorruption)
			envSettings.bloodRain.current = Mathf.Round(corruption / envSettings.bloodRain.divider) * envSettings.bloodRain.frequency;
		else
			envSettings.bloodRain.current = 0;
	}	

	private void CalculateFogDensity(){
		if (corruption <= envSettings.fog.maximumCorruption && corruption > envSettings.fog.minimumCorruption)
			envSettings.fog.current = Mathf.Round(corruption / envSettings.fog.divider) * envSettings.fog.density;
		else
			envSettings.fog.current = 0;
	}

}
