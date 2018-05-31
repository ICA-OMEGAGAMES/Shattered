using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	public CameraFollow camFollow;
	public CameraCollision camCollision;
	public CharacterMovement charMovement;

	private Camera mainCamera;


	void Start(){
		mainCamera = Camera.main;
	}
		
	void OnTriggerStay(Collider other){
		Debug.Log ("Entered Trigger");
		if (other.CompareTag (Constants.ZOOM_TAG)) {
			Debug.Log ("In zoom trigger");
			if (Input.GetButtonDown (Constants.PICKUP_BUTTON)){
				if( mainCamera.enabled) {
					Debug.Log ("Zoom in");
					camFollow.enabled = false;
					camCollision.enabled = false;
					charMovement.enabled = false;
					other.transform.GetChild(0).GetComponent<Camera>().enabled = true;
					mainCamera.enabled = false;
				} else if (!mainCamera.enabled){
					Debug.Log ("Zoom out");
					camFollow.enabled = true;
					camCollision.enabled = true;
					mainCamera.enabled = true;
					charMovement.enabled = true;
					other.transform.GetChild(0).GetComponent<Camera>().enabled = false;

				}
			}
		}
	}
}
