using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButton : MonoBehaviour
{
    public Button unlockButton;
    public Text unlockButtonText;
    public GameObject Malphas;
    private SkillButton currentSkillButton;


    void Start()
    {
        unlockButton.onClick.AddListener(BuySkill);
        Malphas = GameObject.Find("Malphas");
    }

    void OnEnable()
    {
        SkillButton.OnSkillSelected += SetButton;
    }


    void OnDisable()
    {
        SkillButton.OnSkillSelected -= SetButton;
    }

    // Here we set the current selected Skill Button..
    public void SetButton(SkillButton button)
    {
        currentSkillButton = button;

        // if the skill is unlocked, disable the Button and he is purchased
        if (SkillTreeReader.Instance.IsSkillUnlocked(currentSkillButton.skillId))
        {
            unlockButton.interactable = false;
            unlockButtonText.text = "PURCHASED";
        }
        else
        {
            unlockButton.interactable = true;
            unlockButtonText.text = "UNLOCK";
        }
    }

    // Here we buy or unlock the skill
    public void BuySkill()
    {
        if (SkillTreeReader.Instance.UnlockSkill(currentSkillButton.skillId))
        {
            PlayerPrefs.SetInt("Score", SkillTreeReader.Instance.availablePoints);
            currentSkillButton.skillHub.RefreshButtons();

            if (SkillTreeReader.Instance.IsSkillUnlocked(currentSkillButton.skillId))
            {
                unlockButton.interactable = false;
                unlockButtonText.text = "PURCHASED";

                if(Malphas != null)
                {
                    //According to UnitysForm game object dont HAVE to be active to get to their scripts, not able to test
                    Malphas.GetComponent<MalphasScript>().LearnSkill(TeachMalphas(currentSkillButton.skillId));
                }
            }
        }
    }

    public string TeachMalphas(int skillID) {
        switch (skillID)
        {
            case 0:
                return "Teleport";
            case 1:
                return "Barrier";
            case 2:
                return "PsychicScream";
            case 3:
                return "DevineAura";
            case 4:
                return "DemonicWave";
            case 5:
                return "Possess";
            case 6:
                return "DarkClaw";
            default:
                return "None";
        }

    }
}
