using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleIngameMenu : MonoBehaviour
{
    public GameObject ingameMenu;
	public GameObject menuCanvas;
	public GameObject graphicsCanvas;
	public GameObject audioCanvas;

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
        if (Input.GetButtonDown(Constants.MENU_BUTTON))
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
			menuCanvas.SetActive(active);
			graphicsCanvas.SetActive(!active);
			audioCanvas.SetActive(!active);
            Time.timeScale = 0f;
        }
        else
        {

            ingameMenu.SetActive(active);
            Time.timeScale = 0f;

        }
    }
}
