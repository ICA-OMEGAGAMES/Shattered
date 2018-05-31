using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Script to start the game by clicking on play
public class LoadSceneOnClick : MonoBehaviour {

	public int sceneIndex;

	void Start()
	{
		GetComponent<Button>().onClick.AddListener(delegate {LoadByIndex(sceneIndex);});
	}

	public void LoadByIndex(int index)
	{
		SceneManager.LoadScene(index);
	}
}
