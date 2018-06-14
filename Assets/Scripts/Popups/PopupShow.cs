using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupShow : MonoBehaviour {

	public GameObject popup; 

	private bool popupShown = false;

	void Start (){
		popup.SetActive (false);
	}

	void OnTriggerEnter(Collider other){
		if(!popupShown)
			StartCoroutine (ShowPopup());
	}

	IEnumerator ShowPopup()
	{
		popupShown = true;
		popup.SetActive (true);
		yield return new WaitForSeconds(4);
		popup.SetActive(false);
	}
}
