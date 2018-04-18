using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {
	public ItemScriptableObject item;

	public string GetName(){return item.name;}
	public string GetDescription(){return item.description;}
	public Image GetImage(){return item.image;}
	public bool CanBePickedUp(){return item.interactable;}
}
