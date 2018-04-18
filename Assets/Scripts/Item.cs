using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (menuName = "Shattered/Item")]
public class ItemScriptableObject : ScriptableObject {

	public string name = "Item name.";
	public string description = "Item description.";

	public bool interactable = false;

	public Image image = null;

}
