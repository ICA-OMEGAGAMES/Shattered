using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSkilltree : MonoBehaviour {
	public GameObject ingameMenu;
	private SkillTreeMenu skillTreeMenu;

	void Start () {
		GetComponent<Button>().onClick.AddListener(delegate{SwitchScreen();});
	}

	public void CheckSkilltree()
	{
		skillTreeMenu = GameObject.FindObjectOfType<SkillTreeMenu>();
		if(!skillTreeMenu.IsFirstSkillUnlocked())
		{
			GetComponent<Button>().interactable = false;
			GetComponentInChildren<Text>().color = Color.grey;
		} else 
		{
			GetComponent<Button>().interactable = true;
			GetComponentInChildren<Text>().color = Color.white;
		}
	}
	
	void SwitchScreen () {
		skillTreeMenu.Pause();
	}
}