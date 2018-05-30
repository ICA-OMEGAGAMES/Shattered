using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAudio : MonoBehaviour {

	public AudioClip effect;

	private AudioSource audioSource;
	private bool effectPlaying = false;

	void Start (){
		audioSource = GetComponent<AudioSource> ();
	}

	public void InvokePlayEffectCoroutine()
	{
		if (!effectPlaying)
			StartCoroutine (PlayEffect ());
			
	}

	IEnumerator PlayEffect(){
		effectPlaying = true;

		audioSource.clip = effect;
		audioSource.Play ();
		yield return new WaitForSeconds (audioSource.clip.length);

		effectPlaying = false;
	}
}
