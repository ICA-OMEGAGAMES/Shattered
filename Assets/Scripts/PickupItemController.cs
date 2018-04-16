using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupItemController : MonoBehaviour {

    public Canvas game_canvas;
    public Text pickup_text;
    public SystemObject character_settings;
    public Transform player;
	// Use this for initialization
	void Start () {

		if (!player)
        {
            player = GameObject.Find("Player Controller").transform;
        }

        
        pickup_text.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (this.isActiveAndEnabled)
        {
            if (Vector3.Distance(player.position, this.transform.position) <= 2.0f)
            {
                displayPickupMessage();
            }
            else
            {
                resetMessage();
            }
        }
        else
        {
            resetMessage();
        }

        if (character_settings.current_character == 0)
        {
            player = GameObject.Find("Yin").transform;
        }
        else
        {
            player = GameObject.Find("Yang").transform;
        }
    }

    private void displayPickupMessage()
    {
        pickup_text.text = "Press E to pickup: " + this.name;
    }

    private void resetMessage()
    {
        pickup_text.text = "";
    }
}
