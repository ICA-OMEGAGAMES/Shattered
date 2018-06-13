using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SkipCutscene : SceneTransition
{
    public GameObject skipText;

    void Update()
    {
        StartCoroutine(ShowSkippingText());
        
        if (Input.GetButtonDown("Submit"))
        {
			GetComponent<PlayVideo> ().image.enabled = false;
			StartCoroutine (LoadingScreen());
        }
    }

    IEnumerator ShowSkippingText()
    {
        yield return new WaitForSeconds(2);
        skipText.SetActive(false);
    }
}
