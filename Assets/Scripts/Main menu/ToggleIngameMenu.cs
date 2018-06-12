using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity.Shattered;

public class ToggleIngameMenu : MonoBehaviour
{
    public GameObject ingameMenu;
	public GameObject menuPanel;
	public GameObject graphicsPanel;
	public GameObject audioPanel;
    public PlayVideo backgroundVideo;

    public static bool toggleIngameMenuActive = false;

	void Start()
	{
		Button button = GetComponent<Button>();
		if(button != null)
		{
			button.onClick.AddListener(delegate { SwitchScreen(); });
		}
	}

    void Update()
    {
        if (Input.GetButtonDown(Constants.MENU_BUTTON) && !SkillTreeMenu.skillTreeMenuIsActive && !DialogueRunner.isRunning)
        {
            SwitchScreen();
        }
    }

    void SwitchScreen()
    {
        bool active = !ingameMenu.activeSelf;
        if (active && !SkillTreeMenu.skillTreeMenuIsActive)
        {
            ingameMenu.SetActive(active);
			menuPanel.SetActive(active);
            GameObject.FindObjectOfType<OpenSkilltree>().CheckSkilltree();
			backgroundVideo.StartVideo();
            graphicsPanel.SetActive(!active);
			audioPanel.SetActive(!active);
            Time.timeScale = 0f;
        }
        else
        {
            backgroundVideo.StopVideo();
            ingameMenu.SetActive(active);
            Time.timeScale = 1f;
        }
        toggleIngameMenuActive = active;
    }
}