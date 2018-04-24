using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ControlsSettingsManager : MonoBehaviour
{
    public ControlButton[] controlButtons;
    public Button saveAndBackButton;
	public Button clearButton;

	private bool waitingForKey;
	private string keyToChange;

    void OnEnable()
    {
		if(!ControlsSettings.Instance.Initialized)
		{
        	ControlsSettings.Instance.Initialize(controlButtons);
		} else 
		{
			ClearChanges();
		}
        foreach (ControlButton button in controlButtons)
        {
			button.button.GetComponentInChildren<Text>().text = button.key.ToUpper();
            button.button.onClick.AddListener(delegate { UpdateMapping(button.name); });
        }
		
		saveAndBackButton.onClick.AddListener(delegate { SaveSettings(); });
		clearButton.onClick.AddListener(delegate { ClearChanges(); });
    }

    private void UpdateMapping(string name)
    {
		waitingForKey = true;
		keyToChange = name;
    }

    void OnGUI()
    {
        Event e = Event.current;
		if(e != null && e.isKey && waitingForKey)
		{
			int index = GetIndex(keyToChange);
			controlButtons[index].key = e.keyCode.ToString();
			controlButtons[index].button.GetComponentInChildren<Text>().text = e.keyCode.ToString();
			waitingForKey = false;
			keyToChange = null;
		}
    }

    private void SaveSettings()
    {
		ControlsSettings.Instance.Initialize(controlButtons);
		string jsonData = "{\n";
		foreach(KeyValuePair<string, KeyCode> control in ControlsSettings.Instance.controlDictionary)
		{
			jsonData += "\"" + control.Key + "\": \"" + control.Value.ToString() + "\",\n";
		}
		jsonData += "}";
  		File.WriteAllText (Application.persistentDataPath + "/controlsettings.json", jsonData);
    }

	private void ClearChanges()
    {
		int i = 0;
		foreach(KeyValuePair<string, KeyCode> button in ControlsSettings.Instance.controlDictionary)
		{
			controlButtons[i].key = button.Value.ToString().ToUpper();
			i++;
		}
    }

	private int GetIndex(string name)
    {
		int i = 0;
        foreach(ControlButton button in controlButtons)
		{
			if(button.name.Equals(name)){
				return i;
			}
			i++;
		}
		return -1;
    }
}
