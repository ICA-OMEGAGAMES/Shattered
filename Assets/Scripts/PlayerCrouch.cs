using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour {

    public GameObject player;
    public Animator anim;

    private int crouch;
    private int idle;
    private int crouch_walk;
    private bool crouched;

	// Use this for initialization
	void Start () {
	    if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }	

        if (!anim)
        {
            anim = player.GetComponent<Animator>();
        }

        crouch = Animator.StringToHash("Crouch");
        idle = Animator.StringToHash("Idle");
        crouch_walk = Animator.StringToHash("crouched_walk");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Crouch") == 1)
        {
            Debug.Log("crouching");
            anim.CrossFade(crouch, .01f);
            crouched = true;
        }
        else
        {
            crouched = false;
            anim.CrossFade(idle, .01f);
        }

        if (Input.GetAxisRaw("Vertical") !=0 && crouched == true)
        {
            anim.CrossFade(crouch_walk, .5f);
        }
	}
}
