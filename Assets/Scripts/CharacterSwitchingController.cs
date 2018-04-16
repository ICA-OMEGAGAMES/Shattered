using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitchingController : MonoBehaviour {


    public GameObject player_parent;
    public GameObject yin;
    public GameObject yang;
    public SystemObject settings;

   // Use this for initialization
	void Start () {

        characterSwitch();
   	}
	
	// Update is called once per frame
	void Update () {
		
        //if (Input.GetAxisRaw("Switch Character") == 1)
        if(Input.GetKeyDown(KeyCode.Q))
        {
            characterSwitch();
        }
	}

    void characterSwitch()
    {
        //switch the active states of the player character
        if (settings.current_character == 0)
        {
            //save the current velocity of the first character
            Vector3 current_velocity = yin.GetComponent<Rigidbody>().velocity;

            //change which character model is active.
            yin.SetActive(false);
            yang.SetActive(true);

            //set position and rotation to the previous character
            yang.transform.position = yin.transform.position;
            yang.transform.rotation = yin.transform.rotation;

            //set the velocity of the switched character to the previous player;
            yang.GetComponent<Rigidbody>().velocity = current_velocity;

            //set scriptable object value of which character is active
            settings.current_character = 1;
        }
        else
        {
            //save the current velocity of the first character
            Vector3 current_velocity = yin.GetComponent<Rigidbody>().velocity;

            //change which character model is active.
            yin.SetActive(true);
            yang.SetActive(false);

            //set position and rotation to the previous character
            yin.transform.position = yang.transform.position;
            yin.transform.rotation = yang.transform.rotation;


            //set the velocity of the switched character to the previous player;
            yang.GetComponent<Rigidbody>().velocity = current_velocity;

            //set scriptable object value of which character is active
            settings.current_character = 0;
        }
    }
}
