using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity.Shattered;

public class SkillTreeMenu : MonoBehaviour
{
    public static bool skillTreeMenuIsActive = false;

    public GameObject skillTreeMenu;
    public GameObject skillDetails;

    public Text displayAvailablePoints;

    private bool firstSkillUnlocked = false;
    public Button backButton;

    public PlayVideo backgroundVideo;

    // Update is called once per frame
    public void Update()
    {
        displayAvailablePoints.text = "skill points " + SkillTreeReader.Instance.availablePoints;

        //Check #3.1 to
        if (Input.GetButtonDown(Constants.SKILL_TREE_BUTTON) && firstSkillUnlocked)
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
        backgroundVideo.StopVideo();
    }

    public void Pause()
    {
        skillTreeMenu.SetActive(true);
        Time.timeScale = 0f;
        skillTreeMenuIsActive = true;
        backgroundVideo.StartVideo();
        skillDetails.SetActive(false);
    }

    public void openWithinDialogueSystem()
    {
        //#1 close DialogueCanvas
        FindObjectOfType<DialogueRunner>().dialogueCanvas.SetActive(false);

        //#2 open this Menu
        this.Pause();

        //Disable Back Button
        backButton.interactable = false;

        //#3 how can i go back?
        //      if i unlock the first skill (In a while? or couroutine?) --> while if IsSkillUnlocked(skill01) unlocked do nothing other wise go to #4
        //  #3.1 We also need to can be opened now set, because after the tutorial we can open the skill tree by pressing i
        StartCoroutine(WaitUntilSkillIsUnlocked());
    }

    IEnumerator WaitUntilSkillIsUnlocked()
    {
        while (!SkillTreeReader.Instance.IsSkillUnlocked(0))
        {
            yield return null;
        }

        //#4 Close this Menu
        this.Resume();

        //#5 Activate the Dialogue System
        //      We need to check what if the right text will be shown later
        //      because I don't know if we can pause the dialogue, but normally i think it should work
        FindObjectOfType<DialogueRunner>().dialogueCanvas.SetActive(true);

        //#6 Set firstSkillUnlocked to true
        firstSkillUnlocked = true;

        //Able Back Button
        backButton.interactable = true;
    }

    public bool IsFirstSkillUnlocked()
    {
        return firstSkillUnlocked;
    }
}
