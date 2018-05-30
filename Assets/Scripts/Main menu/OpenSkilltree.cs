using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSkilltree : MonoBehaviour {
	public GameObject ingameMenu;
	public SkillTreeMenu skillTreeMenu;

	void Start () {
		GetComponent<Button>().onClick.AddListener(delegate{SwitchScreen();});
		if(!skillTreeMenu.IsFirstSkillUnlocked())
		{
			GetComponent<Button>().interactable = false;
			GetComponentInChildren<Text>().color = Color.grey;
		}
	}
	
	void SwitchScreen () {
		skillTreeMenu.Pause();
		ingameMenu.SetActive(false);
	}
}
