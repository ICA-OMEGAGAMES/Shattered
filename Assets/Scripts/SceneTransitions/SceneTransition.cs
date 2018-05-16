using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    //index of the current string
    private int current_scene;

	// Use this for initialization
	void Start () {
        int current_scene = SceneManager.GetActiveScene().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
	    //if (Input.GetButton("Pickup"))
     //   {
     //       SceneManager.LoadScene(next_scene);
     //   }	
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(current_scene +1);
        }
    }




}
