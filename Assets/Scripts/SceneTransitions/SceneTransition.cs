using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    //index of the current string
    private int currentScene;

	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == Constants.PLAYER_TAG)
        {
            //increment by 1 to load the next scene.
            SceneManager.LoadScene(currentScene +1);
        }
    }




}
