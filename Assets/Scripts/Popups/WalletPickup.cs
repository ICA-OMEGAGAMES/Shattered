using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletPickup : MonoBehaviour {

	private bool pickedup = false;
	private bool playerInRange = false;

	void Update(){
		if (playerInRange && !pickedup) {
			if (Input.GetButtonDown (Constants.PICKUP_BUTTON)) {
				gameObject.SetActive (false);
				pickedup = true;
			} 
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag (Constants.PLAYER_TAG)) {
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag (Constants.PLAYER_TAG)) {
			playerInRange = false;
		}
	}
}
