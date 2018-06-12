using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioFacade : MonoBehaviour {

	public List<AudioClip> walkingSounds;
	public bool isRunning = false;

	private AudioSource audioSource;
	private bool walkingSoundsPlaying = false;


	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	public void InvokeWalkingSoundsCoroutine(){
		if (!walkingSoundsPlaying)
			StartCoroutine (PlayWalkingSounds ());
	}

	IEnumerator PlayWalkingSounds(){
		walkingSoundsPlaying = true;

		if (isRunning)
			audioSource.pitch = 1.0f;
		else 
		// slow down
		audioSource.pitch = 0.6f;
		
		int randIndex = Random.Range (0, walkingSounds.Count);
		if(audioSource.clip == walkingSounds [randIndex])
			randIndex = Random.Range (0, walkingSounds.Count);
		audioSource.clip = walkingSounds [randIndex];
		audioSource.Play ();
		yield return new WaitForSeconds (audioSource.clip.length);
		walkingSoundsPlaying = false;

	}
}
