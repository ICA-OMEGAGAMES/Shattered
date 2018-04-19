using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

	public ItemSettings item;

	// Not in the settings because this is specific for this item only. 
	private string 	itemCode = "";

	void Start () { GenerateItemCode ();}

	public string GetItemCode(){return itemCode;}
	public string GetName(){return item.name;}
	public string GetDescription(){return item.description;}
	public Image GetImage(){return item.image;}
	public bool CanBePickedUp(){return item.interactable;}

	private void GenerateItemCode(){

		int itemCodeLength = 5;
		var characters = "ABCDEFGHIJKLMNOPQRSTUVW0123456789".ToCharArray();

		while (itemCode.Length < itemCodeLength)
			itemCode += characters [Random.Range (0, characters.Length)]; 
	}

}
