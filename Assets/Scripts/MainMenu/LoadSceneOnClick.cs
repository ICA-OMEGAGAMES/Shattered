using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Script to start the game by clicking on play
public class LoadSceneOnClick : MonoBehaviour {

	public Button startButton;
	public int sceneIndex;

	void Start()
	{
		startButton.onClick.AddListener(delegate {LoadByIndex(sceneIndex);});
	}

	public void LoadByIndex(int index)
	{
		SceneManager.LoadScene(index);
	}
}
