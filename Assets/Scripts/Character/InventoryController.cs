using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryController : MonoBehaviour {

    public GameObject equipedWeapon = null;

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
        if (other.gameObject.CompareTag(Constants.WEAPON_TAG))
        {
            var weapon = other.GetComponent<Weapon>();
            if (Input.GetButtonDown(Constants.PICKUP_BUTTON) && weapon.CanBePickedUp())
            {
                WeaponHandler weaponHandler = GameObject.Find("WeaponHandler").GetComponent<WeaponHandler>();
                print(weaponHandler.currentWeapon);
                
                if (weaponHandler.currentWeapon == null)
                {
                    weaponHandler.EquipWeapon(other.gameObject);
                }
                else if (weaponHandler.currentWeapon != other.gameObject)
                {
                    weaponHandler.EquipWeapon(other.gameObject);
                }
            }
        }
	}

	void RemoveItemFromInventory(string itemCode){
		foreach(Item item in inventory){
			if (item.GetItemCode () == itemCode)
				inventory.Remove (item);
		}
	}

}
