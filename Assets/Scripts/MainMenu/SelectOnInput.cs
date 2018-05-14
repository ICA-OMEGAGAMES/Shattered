using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Script to enable selection with controllers and/or mouse/keyboard
public class SelectOnInput : MonoBehaviour {

	public EventSystem eventSystem;
	public GameObject selectedObject;

	private bool buttonSelected;

	void Update () {
		if(Input.GetAxisRaw(Constants.VERTICAL_AXIS) != 0 && !buttonSelected)
		{
			eventSystem.SetSelectedGameObject(selectedObject);
			buttonSelected = true;
		}
	}

	private void OnDisable(){
		buttonSelected = false;
	}
}
