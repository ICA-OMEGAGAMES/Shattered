using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour {

	private ParticleSystem rain;

	public ParticleSystem heavy;
	public ParticleSystem fog;
	public ParticleSystem plane;
	public ParticleSystem ripples;
	public ParticleSystem splash;

	void Start(){
		rain = GetComponent<ParticleSystem> ();
	}

	public void PlayRainSideEffects(){
		fog.Play ();
		plane.Play ();
		ripples.Play ();
		splash.Play ();
	}

	public void StopRainSideEffects(){
		fog.Stop (true, ParticleSystemStopBehavior.StopEmittingAndClear);
		plane.Stop (true, ParticleSystemStopBehavior.StopEmittingAndClear);
		ripples.Stop (true, ParticleSystemStopBehavior.StopEmittingAndClear);
		splash.Stop (true, ParticleSystemStopBehavior.StopEmittingAndClear);
	}

	public void setEmission(float rate){ 
		var emission = rain.emission;
		emission.rateOverTime = rate;
	}	
}
