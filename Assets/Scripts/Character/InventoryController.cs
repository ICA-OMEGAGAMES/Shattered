using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {



	private List<Item> inventory = new List<Item> ();

	void OnTriggerStay (Collider other)
	{	
		if (other.gameObject.CompareTag (Constants.ITEM_TAG)) {
			var item = other.GetComponent<Item> ();

			if (Input.GetButtonDown (Constants.PICKUP_BUTTON) && item.CanBePickedUp ()) {
				other.gameObject.SetActive (false);
				inventory.Add (item);
			}
		
			// TODO: Display text on screen once dialog is implemented.
			if(Input.GetButtonDown(Constants.EXAMINE_BUTTON))
				print(item.GetName() + ": " + item.GetDescription());	
		}
			
	}

	void RemoveItemFromInventory(string itemCode){
		foreach(Item item in inventory){
			if (item.GetItemCode () == itemCode)
				inventory.Remove (item);
		}
	}

}
