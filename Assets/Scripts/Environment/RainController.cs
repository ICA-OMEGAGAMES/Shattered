using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {

	public EnvironmentSettings envSettings;

	private Rain rain;
	private Rain bloodRain;

	void Awake(){
		rain = GameObject.Find ("Regular").GetComponent<Rain> ();
		bloodRain = GameObject.Find ("Blood").GetComponent<Rain> ();
	}

	void Update(){
		rain.setEmission (envSettings.rain.current);
		if (envSettings.rain.current != 0) {
			rain.PlayRainSideEffects ();

			if (envSettings.rain.current >= envSettings.rain.maximumCorruption / 2)
				rain.heavy.Play ();
		} else {
			rain.heavy.Stop ();
			rain.StopRainSideEffects ();
		}


		bloodRain.setEmission (envSettings.bloodRain.current);
		if (envSettings.bloodRain.current != 0) {
			bloodRain.PlayRainSideEffects ();
			if (envSettings.bloodRain.current >= envSettings.bloodRain.maximumCorruption / 2)
				bloodRain.heavy.Play ();
		} else{
			bloodRain.StopRainSideEffects ();
			bloodRain.heavy.Stop ();
		}
	
	}

		

}
