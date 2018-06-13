using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneTransition : MonoBehaviour {

	public Slider slider; 
	public GameObject loadingScreenObj;
	public PlayVideo backgroundVideo;

    //index of the current string
    private int currentScene;
	private AsyncOperation async;

	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == Constants.PLAYER_TAG)
        {
			StartCoroutine(LoadingScreen());
		}
    }


	public IEnumerator LoadingScreen(){
		loadingScreenObj.SetActive (true);
		backgroundVideo.StartVideo();
		async = SceneManager.LoadSceneAsync (currentScene +1);
		async.allowSceneActivation = false;

		while (async.isDone == false) {
			slider.value = async.progress;
			if (async.progress == 0.9f) {
				slider.value = 1f;
				async.allowSceneActivation = true;
			}
			yield return null;
		}
	}

}
