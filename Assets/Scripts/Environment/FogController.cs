using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {

	public EnvironmentSettings envSettings;

	public Material light;
	public Material medium;
	public Material heavy;

	private static float maxDensity = 1;
	private static float modes = 3;

	private static float maxLightDensity = maxDensity / modes;
	private static  float maxMediumDensity = maxDensity - maxLightDensity;

	void Start(){
		RenderSettings.fog = true;
	}

	void Update () {
		setFogDensity (envSettings.fog.current);	
		adjustFogColor ();
	}

	public void setFogDensity(float density){
		RenderSettings.fogDensity = density;
	}

	private void adjustFogColor(){
		if (RenderSettings.fogDensity <= maxLightDensity) {
			RenderSettings.fogColor = light.color;
		}
		else if (RenderSettings.fogDensity <= maxMediumDensity && RenderSettings.fogDensity > maxLightDensity) {
			RenderSettings.fogColor = medium.color;
		}else {
			RenderSettings.fogColor = heavy.color;
		}
	}
}
