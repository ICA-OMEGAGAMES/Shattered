using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour {

    //Will be used when animation is used.
    public Animator animator;
    public GameObject player;
    public int distance;
    private Vector3 current_position;
    
    //Bool to check if the player is rolling.
    private bool is_rolling;

    //helper to track the axis.
    private string axis;

    //Store either 1 or -1 for the positive or negative direction of the player's input.
    private int direction;

    // Use this for initialization
    void Start() {

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        current_position = player.transform.position;
        is_rolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        // Forward dodge check
        if (Input.GetAxisRaw("Dodge") == 1 && is_rolling == false && Input.GetAxisRaw("Vertical") == 1)
        {
            axis = "z";
            direction = 1;
            StartCoroutine(Roll());
        }
        //Backward dodge check
        else if (Input.GetAxisRaw("Dodge") == 1 && is_rolling == false && Input.GetAxisRaw("Vertical") == -1)
        {
            axis = "z";
            direction = -1;
            StartCoroutine(Roll());
        }
        //Right dodge/step check
        else if (Input.GetAxisRaw("Dodge") == 1 && is_rolling == false && Input.GetAxisRaw("Horizontal") == 1)
        {
            axis = "x";
            direction = 1;
            StartCoroutine(Roll());
        }
        //Left dodge/step check
        else if (Input.GetAxisRaw("Dodge") == 1 && is_rolling == false && Input.GetAxisRaw("Horizontal") == -1)
        {
            axis = "x";
            direction = -1;
            StartCoroutine(Roll());
        }
    }

    //Dodge Coroutine
    IEnumerator Roll(){

        Debug.Log("Axis: " + axis);
        Debug.Log("direction: " + direction);

        if (axis == "x")
        {
            current_position.x += distance * direction;
        }
        else if (axis == "z")
        {
            current_position.z += distance * direction;
        }
        player.transform.Translate(current_position * Time.deltaTime);
        is_rolling = true;
        yield return new WaitForSeconds(1f);
        StopCoroutine(Roll());
        is_rolling = false;
    }
}
