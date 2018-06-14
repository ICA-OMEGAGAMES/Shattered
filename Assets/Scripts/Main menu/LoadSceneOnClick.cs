using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Script to start the game by clicking on play
public class LoadSceneOnClick : SceneTransition {

	public int sceneIndex;
	public AudioSource clipToPlayOnClick;

	void Start()
	{
		GetComponent<Button>().onClick.AddListener(delegate {LoadByIndex(sceneIndex);});
		clipToPlayOnClick = GetComponent<AudioSource> ();
	}

	public void LoadByIndex(int index)
	{
		StartCoroutine (PlayStartSound());

	}

	IEnumerator PlayStartSound(){
		clipToPlayOnClick.Play ();
		yield return new WaitForSeconds (.5f);
		StartCoroutine (LoadingScreen());
	}
}
