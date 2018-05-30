using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public int skillId;

    public Color unlockedColor;

    public SkillHub skillHub;

    public Text descriptionText;
    public Text skillCost;
    public Button unlockButton;

    public delegate void SkillSelectedAction(SkillButton button);
    public static event SkillSelectedAction OnSkillSelected;

    public GameObject skillDetails;

    // private Image image;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        RefreshState();
    }

    public void RefreshState()
    {
        if (SkillTreeReader.Instance.IsSkillUnlocked(skillId))
        {
            var colors = button.colors;
            colors.normalColor = Color.yellow;
            button.colors = colors;
        }
        else if (!SkillTreeReader.Instance.CanSkillBeUnlocked(skillId))
            button.interactable = false;
        else
            button.interactable = true;
    }

    public void updateSkillDetails()
    {
        // displayDetails
        skillDetails.SetActive(true);

        // showDescription
        descriptionText.text = SkillTreeReader.Instance.getDescription(skillId);

        // showSkillCost
        if (SkillTreeReader.Instance.getSkillCost(skillId) == "0")
        {
            skillCost.text = "";

        }
        else
        {
            skillCost.text = "Cost: " + SkillTreeReader.Instance.getSkillCost(skillId);

        }

        // activateUnlockButton
        OnSkillSelected(this);
    }
}