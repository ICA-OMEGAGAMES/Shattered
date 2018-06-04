using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script to quit the game when quit is clicked
public class QuitOnClick : MonoBehaviour {

	private Button quitButton;

	void Start()
	{
		quitButton.onClick.AddListener(delegate{Quit();});
	}

	public void Quit()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
