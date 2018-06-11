using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButton : MonoBehaviour
{
    public Button unlockButton;
    public Text unlockButtonText;
    private SkillButton currentSkillButton;

    void Start()
    {
        unlockButton.onClick.AddListener(BuySkill);
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
            }
        }
    }
}
