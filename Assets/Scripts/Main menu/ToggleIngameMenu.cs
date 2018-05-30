using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleIngameMenu : MonoBehaviour {

	public Canvas menuCanvas;
	public GameObject background;

	void Start () {
		GetComponent<Button>().onClick.AddListener(delegate{SwitchScreen();});
	}
	
	void SwitchScreen () {
		menuCanvas.enabled = !menuCanvas.enabled;
		background.SetActive(!background.activeSelf);
	}
}
