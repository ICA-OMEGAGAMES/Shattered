using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script that enables the new and disables the previous menu screen when selecting a settings menu
public class SwitchMenuScreen : MonoBehaviour {

	public Button button;
	public GameObject objectToEnable;
	public GameObject objectToDisable;

	void Start () {
		button.onClick.AddListener(delegate{SwitchScreen();});
	}
	
	void SwitchScreen () {
		objectToEnable.SetActive(true);
		objectToDisable.SetActive(false);
	}
}
