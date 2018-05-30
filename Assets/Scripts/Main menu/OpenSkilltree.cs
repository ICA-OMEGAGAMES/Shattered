using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSkilltree : MonoBehaviour {

	public Canvas menuCanvas;
    public Canvas skillTreeCanvas;
	public GameObject menuBackground;
    public GameObject skillTreeBackground;

	void Start () {
		GetComponent<Button>().onClick.AddListener(delegate{SwitchScreen();});
	}
	
	void SwitchScreen () {
		menuCanvas.enabled = false;
		menuBackground.SetActive(false);
        skillTreeCanvas.enabled = true;
        skillTreeBackground.SetActive(true);
	}
}
