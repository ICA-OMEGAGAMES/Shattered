using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {

	public ParticleSystem ps;

	void Start (){
		ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
	}

	void OnTriggerStay(Collider other){
		ps.Play(true);
	}

	void OnTriggerExit(Collider other){
		ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
	}
}
