using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventManager : MonoBehaviour {

    public bool is_at_end;
    public string next_scene;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (is_at_end == true)
        {
            if (Input.GetButton("Pickup"))
            {
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            is_at_end = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            is_at_end = false;
        }
    }
}
