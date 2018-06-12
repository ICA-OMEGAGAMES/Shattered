using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	public CameraFollow camFollow;
	public CameraCollision camCollision;
	public AppartmentMusicSwap apSwap;
    public DevonIndoorScript dis;
	public Camera zoom;

	private Camera mainCamera;


	void Start(){
		mainCamera = Camera.main;
	}
		
	void OnTriggerStay(Collider other){
		if (other.CompareTag (Constants.PLAYER_TAG)) {
			if (Input.GetButtonDown (Constants.PICKUP_BUTTON)){
				if( mainCamera.enabled) {
					camFollow.enabled = false;
					camCollision.enabled = false;
					zoom.enabled = true;
					mainCamera.enabled = false;
                    dis.characterControllable = false;
					// for now this is only used for the shrine, if used for more don't use the music swap here!
					apSwap.ChangeMusic();
				} else if (!mainCamera.enabled){
                    dis.characterControllable = true;
					camFollow.enabled = true;
					camCollision.enabled = true;
					mainCamera.enabled = true;
					zoom.enabled = false;
					apSwap.ChangeMusic(); 
				}
			}
		}
	}
}
