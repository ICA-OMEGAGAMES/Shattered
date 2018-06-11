using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour
{
    public GameObject skipText;
    private int currentScene;
    private bool textIsVisible;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        textIsVisible = false;
    }

    void Update()
    {
        if (!textIsVisible && Input.anyKeyDown)
        {
            skipText.SetActive(true);
            textIsVisible = true;
            StartCoroutine(ShowSkippingText());
        }

        if (textIsVisible && Input.GetButtonDown("Submit"))
        {
            Debug.Log("StartSkipping");
            SceneManager.LoadScene(currentScene + 1);
        }
    }

    IEnumerator ShowSkippingText()
    {
        yield return new WaitForSeconds(2);

        textIsVisible = false;
        skipText.SetActive(false);
    }
}
