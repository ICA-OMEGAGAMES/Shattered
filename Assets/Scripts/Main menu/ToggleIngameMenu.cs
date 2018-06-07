using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleIngameMenu : MonoBehaviour
{
    public GameObject ingameMenu;
	public GameObject menuPanel;
	public GameObject graphicsPanel;
	public GameObject audioPanel;
    public PlayVideo backgroundVideo;

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
        if (!SkillTreeMenu.skillTreeMenuIsActive && Input.GetButtonDown(Constants.MENU_BUTTON))
        {
            SwitchScreen();
        }
    }

    void SwitchScreen()
    {
        bool active = !ingameMenu.activeSelf;
        if (active)
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
    }
}