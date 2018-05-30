using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public int skillId;

    // public Color unlockedColor;

    public SkillHub skillHub;

    public Text descriptionText;
    public Text skillNameText;
    public Sprite sprite;
    public Image skillImg;
    public Button unlockButton;

    public delegate void SkillSelectedAction(SkillButton button);
    public static event SkillSelectedAction OnSkillSelected;

    public GameObject skillDetails;

    private Button button;
    // private Image img;

    void Start()
    {
        button = GetComponent<Button>();
        // img = GetComponent<Image>();
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
            colors.normalColor = new Color32(255, 255, 255, 255);
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

        // showName
        skillNameText.text = SkillTreeReader.Instance.getSkillName(skillId);

        // setImage
        skillImg.sprite = sprite;

        // activateUnlockButton
        OnSkillSelected(this);
    }
}