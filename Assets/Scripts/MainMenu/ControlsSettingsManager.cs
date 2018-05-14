using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ControlsSettingsManager : MonoBehaviour
{
    public List<ControlButton> controlButtons;
    public List<ControlAxis> controlAxes;
    public Button saveAndBackButton;
    public Button clearButton;
	public Canvas menuCanvas;
	public Canvas popupCanvas;

    private bool isWaitingForKey;
    private string controlToChange;
    private bool isAxisMapping;
    private bool isPositiveAxis;
    private float selectionBlockingTimer;

    void OnEnable()
    {
        // Initialize the dictionary with the buttons specified in inspector and synchronise with ClearChanges()
        ControlSettings.Instance.Initialize(controlButtons, controlAxes);
        ClearChanges();

        //	Update with name of currently selected key and add listeners
        UpdateText();
        RegisterListener();
    }

    void Update()
    {
        selectionBlockingTimer -= Time.deltaTime;
    }

    private void UpdateText()
    {
        controlButtons.ForEach(button => button.assignmentButton.GetComponentInChildren<Text>().text = button.assignedKey.ToUpper());
        
        controlAxes.ForEach(axis => 
		{
            axis.positiveButton.GetComponentInChildren<Text>().text = axis.positiveKey.ToUpper();
            axis.negativeButton.GetComponentInChildren<Text>().text = axis.negativeKey.ToUpper();
        });

    }
    private void RegisterListener()
    {
        controlButtons.ForEach(button => button.assignmentButton.onClick.AddListener(delegate { this.UpdateMapping(button.buttonName, false, false); }));
        
        controlAxes.ForEach(axis =>
        {
            axis.positiveButton.onClick.AddListener(delegate { UpdateMapping(axis.axisName, true, true); });
            axis.negativeButton.onClick.AddListener(delegate { UpdateMapping(axis.axisName, true, false); });
        });

        saveAndBackButton.onClick.AddListener(delegate { SaveSettings(); });
        clearButton.onClick.AddListener(delegate { ClearChanges(); });
		popupCanvas.GetComponentInChildren<Button>().onClick.AddListener(delegate { ClosePopup(); });
    }

    private void UpdateMapping(string controlName,  bool isAxis, bool isPositiveAxis)
    {
        //	sets variables and waiting for key to true so that next OnGui changes key
        if(selectionBlockingTimer > 0)
        {
            return;
        }

        isWaitingForKey = true;
        controlToChange = controlName;
        this.isPositiveAxis = isPositiveAxis;
        isAxisMapping = isAxis;

		//  Show the popup
        popupCanvas.GetComponentInChildren<Text>().text = "Press a key to assign it to this control...";
		menuCanvas.enabled = false;
		popupCanvas.gameObject.SetActive(true);
		
    }

    void OnGUI()
    {
        Event e = Event.current;

        if (!isWaitingForKey || e == null || !(e.type == EventType.KeyDown) || e.keyCode == KeyCode.None)
        {
            //	Event is not a new assignment but something different
            return;
        }

		string key = e.keyCode.ToString();
		
		if(ButtonAlreadyInUse(key))
		{
			popupCanvas.GetComponentInChildren<Text>().text = "This button is already in use. Select a different one.";
            selectionBlockingTimer = 0.5f;
			return;
		}
        
        if (!isAxisMapping)
        {
            //	Assignment is for a button
            AssignButton(key);
            ClosePopup();
            return;
        }

        //	Assignment is for an axis
        AssignAxis(key);
        ClosePopup();
    }

    private void AssignButton(string key)
    {
		int index = GetIndexOfButton(controlToChange);

        if (index == -1)
        {
            Debug.LogError("Error finding the command to be changed.");
            return;
        }

        controlButtons[index].assignedKey = key;
        controlButtons[index].assignmentButton.GetComponentInChildren<Text>().text = key.ToUpper();
    }

    private void AssignAxis(string key)
    {
		int index = GetIndexOfAxis(controlToChange);

        if (index == -1)
        {
            Debug.LogError("Error finding the command to be changed.");
            return;
        }

        if (isPositiveAxis)
        {
            //	Assignment belongs to positive axis
            controlAxes[index].positiveKey = key;
            controlAxes[index].positiveButton.GetComponentInChildren<Text>().text = key.ToUpper();
            return;
        }

        //	Assignment belongs to negative axis
        controlAxes[index].negativeKey = key;
        controlAxes[index].negativeButton.GetComponentInChildren<Text>().text = key.ToUpper();
    }

	private bool ButtonAlreadyInUse(string key)
	{
		return controlButtons.Exists(element => element.assignedKey.ToUpper().Equals(key.ToUpper())) || controlAxes.Exists(element =>  { 
				return element.negativeKey.ToUpper().Equals(key.ToUpper()) || element.positiveKey.ToUpper().Equals(key.ToUpper());
			});
	}

    private void ClosePopup()
    {
        selectionBlockingTimer = 0.5f;
        popupCanvas.gameObject.SetActive(false);
        menuCanvas.enabled = true; 
        controlToChange = null;
        isWaitingForKey = false;
    }

    private void SaveSettings()
    {
        //Updating the dictionary with saved values and saving as json
        ControlSettings.Instance.Update(controlButtons, controlAxes);
        string jsonData = "{\n";
	
        foreach (KeyValuePair<string, KeyCode> control in ControlSettings.Instance.buttonDictionary)
        {
            jsonData += "\"" + control.Key + "\": \"" + control.Value.ToString() + "\",\n";
        }

        foreach (KeyValuePair<string, KeyCode[]> control in ControlSettings.Instance.axisDictionary)
        {
            jsonData += "\"" + control.Key + "\": [\"" + control.Value[0].ToString() + "\", \"" + control.Value[1].ToString() + "\"] ,\n";
        }

        jsonData = jsonData.Substring(0, jsonData.Length - 2);
        jsonData += "\n}";

        File.WriteAllText(Application.persistentDataPath + "/controlsettings.json", jsonData);
    }

    private void ClearChanges()
    {
        //Clear all changes made and load state from dictionary
        List<ControlButton> newControlButtons = new List<ControlButton>();
        List<ControlAxis> newControlAxes = new List<ControlAxis>();

        controlButtons.ForEach(button =>
        {
            ControlButton newButton = new ControlButton();
            newButton.buttonName = button.buttonName;
            newButton.assignedKey = ControlSettings.Instance.buttonDictionary[button.buttonName].ToString().ToUpper();
            newButton.assignmentButton = button.assignmentButton;

            newControlButtons.Add(newButton);
        });

        controlAxes.ForEach(axis =>
        {
            ControlAxis newAxis = new ControlAxis();
            newAxis.axisName = axis.axisName;

            newAxis.positiveKey = ControlSettings.Instance.axisDictionary[axis.axisName][0].ToString().ToUpper();
            newAxis.positiveButton = axis.positiveButton;

            newAxis.negativeKey = ControlSettings.Instance.axisDictionary[axis.axisName][1].ToString().ToUpper();
            newAxis.negativeButton = axis.negativeButton;

            newControlAxes.Add(newAxis);
        });

        controlButtons = newControlButtons;
        controlAxes = newControlAxes;
    }

    private int GetIndexOfButton(string name)
    {
        return controlButtons.FindIndex(element => element.buttonName.Equals(name));
    }

    private int GetIndexOfAxis(string name)
    {
        return controlAxes.FindIndex(element => element.axisName.Equals(name));
    }
}
