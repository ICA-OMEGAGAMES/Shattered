using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	public AudioClip doorClose;
    //index of the current string
    private int currentScene;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
		audioSource = GetComponent<AudioSource> ();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == Constants.PLAYER_TAG)
        {
            //increment by 1 to load the next scene.
			StartCoroutine (TransitionScene ());

        }
    }

	IEnumerator TransitionScene(){

		audioSource.clip = doorClose;
		audioSource.Play ();
		yield return new WaitForSeconds (doorClose.length - .4f);
		SceneManager.LoadScene(currentScene +1);
	}




}
