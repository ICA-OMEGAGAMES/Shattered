using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeMenu : MonoBehaviour
{
    public static bool skillTreeMenuIsActive = false;

    public GameObject skillTreeMenu;
    public GameObject skillDetails;

    public Text displayAvailablePoints;

    // Update is called once per frame
    public void Update()
    {
        displayAvailablePoints.text = "Available Points: " + SkillTreeReader.Instance.availablePoints;

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (skillTreeMenuIsActive)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        skillTreeMenu.SetActive(false);
        Time.timeScale = 1f;
        skillTreeMenuIsActive = false;

        // Disable the details of the skill by closing, so that by opening it again the details are not here
        // they will appear if we select a skill
        skillDetails.SetActive(false);
    }

    public void Pause()
    {
        skillTreeMenu.SetActive(true);
        Time.timeScale = 0f;
        skillTreeMenuIsActive = true;
    }
}
