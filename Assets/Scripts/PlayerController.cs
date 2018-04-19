using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;

	private List<Item> inventory = new List<Item> ();

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();	
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		rb.AddForce(new Vector3(horizontal, 0.0f, vertical) * 10);
	}


	void OnTriggerStay (Collider other)
	{	
		if (other.gameObject.CompareTag ("Item")) {
			var item = other.GetComponent<Item> ();

			if (Input.GetButtonDown ("Pickup") && item.CanBePickedUp ()) {
				other.gameObject.SetActive (false);
				inventory.Add (item);
			}
		
			if(Input.GetButtonDown("Examine"))
				print(item.GetName() + ": " + item.GetDescription());	
		}
			
	}

	// TODO: Might be a better way to do this than with a foreach. Check List doc.
	void RemoveItemFromInventory(string itemCode){
		foreach(Item item in inventory){
			if (item.GetItemCode ().CompareTo (itemCode) == 1)
				inventory.Remove (item);
		}
	}

}
