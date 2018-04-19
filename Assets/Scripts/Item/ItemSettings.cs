using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (menuName = "Shattered/Item")]
public class ItemSettings : ScriptableObject {

	public string itemName = "Item name.";
	public string itemDescription = "Item description.";

	public bool interactable = false;

	public Image image = null;

}
