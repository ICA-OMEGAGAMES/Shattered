using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {


	public AudioClip doorOpen;
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
			SceneManager.LoadScene(currentScene +1);			
			StartCoroutine (PlayDoorSounds ());

        }
    }

	IEnumerator PlayDoorSounds(){

		audioSource.clip = doorOpen;
		audioSource.Play ();
		yield return new WaitForSeconds (doorOpen.length);
		audioSource.clip = doorClose;
		audioSource.Play ();

	}




}
