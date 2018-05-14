using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSettings
{
    public Dictionary<string, KeyCode> buttonDictionary { get; private set; }
    public Dictionary<string, KeyCode[]> axisDictionary { get; private set; }

    private static ControlSettings controlSettings;

    private ControlSettings() { }

    public static ControlSettings Instance
    {
        get
        {
            if (controlSettings == null)
            {
                controlSettings = new ControlSettings();
            }
            return controlSettings;
        }
    }
    public void Initialize(List<ControlButton> controlButtons, List<ControlAxis> controlAxes)
    {
        if(controlButtons.Count < 1 || controlAxes.Count < 1)
        {
            Debug.Log("No buttons or axes specified.");
        }

        if(buttonDictionary == null)
        {
            buttonDictionary = new Dictionary<string, KeyCode>();
        }
        if(axisDictionary == null)
        {
            axisDictionary = new Dictionary<string, KeyCode[]>();
        }

        foreach (ControlButton button in controlButtons)
        {
            try
            {
                if(!buttonDictionary.ContainsKey(button.buttonName))
                {
                    AddButton(button);                
                }
            }
            catch (ArgumentException argEx)
            {
                Debug.LogError("Button " + button.buttonName + ": Key " + button.assignedKey + " not found." + argEx.StackTrace);
            }
        }

        foreach (ControlAxis axis in controlAxes)
        {
            try
            {
                if(!axisDictionary.ContainsKey(axis.axisName))
                {
                    AddAxis(axis);
                }
            }
            catch (ArgumentException argEx)
            {
                Debug.LogError("Button " + axis.axisName + ": Key " + axis.positiveKey + "/" + axis.negativeKey + " not found." + argEx.StackTrace);
            }
        }
    }

    public void Update(List<ControlButton> controlButtons, List<ControlAxis> controlAxes)
    {
        if(controlButtons.Count < 1 || controlAxes.Count < 1)
        {
            Debug.Log("No buttons or axes specified.");
        }

        foreach (ControlButton button in controlButtons)
        {
            try
            {
                if(buttonDictionary.ContainsKey(button.buttonName))
                {
                    buttonDictionary[button.buttonName] = (KeyCode)System.Enum.Parse(typeof(KeyCode), button.assignedKey, true);
                    continue;
                }
                AddButton(button);
            }
            catch (ArgumentException argEx)
            {
                Debug.LogError("Button " + button.buttonName + ": Key " + button.assignedKey + " not found." + argEx.StackTrace);
            }
        }

        foreach (ControlAxis axis in controlAxes)
        {
            try
            {
                if(axisDictionary.ContainsKey(axis.axisName))
                {
                    axisDictionary[axis.axisName] = new KeyCode[]{(KeyCode)System.Enum.Parse(typeof(KeyCode), axis.positiveKey, true), (KeyCode)System.Enum.Parse(typeof(KeyCode), axis.negativeKey, true)};
                    continue;
                }
                AddAxis(axis);
            }
            catch (ArgumentException argEx)
            {
                Debug.LogError("Button " + axis.axisName + ": Key " + axis.positiveKey + "/" + axis.negativeKey + " not found." + argEx.StackTrace);
            }
        }
    }

    public void AddButton(ControlButton button)
    {
        if(buttonDictionary == null)
        {
            buttonDictionary = new Dictionary<string, KeyCode>();
        }
        buttonDictionary.Add(button.buttonName, (KeyCode)System.Enum.Parse(typeof(KeyCode), button.assignedKey, true));
    }

    public void AddAxis(ControlAxis axis)
    {
        if(axis.positiveKey == null || axis.negativeKey == null)
        {
            Debug.LogError("The axis " + axis.axisName + " has to have exactly two keys (positive and negative) assigned to it.");
            return;
        }
        if(axisDictionary == null)
        {
            axisDictionary = new Dictionary<string, KeyCode[]>();
        }

        axisDictionary.Add(axis.axisName, new KeyCode[]{(KeyCode)System.Enum.Parse(typeof(KeyCode), axis.positiveKey, true), (KeyCode)System.Enum.Parse(typeof(KeyCode), axis.negativeKey, true)});
    }

    public bool GetButtonDown(string keyName)
    {
        return Input.GetKeyDown(buttonDictionary[keyName]);
    }

    public bool GetButton(string keyName)
    {
        return Input.GetKey(buttonDictionary[keyName]);
    }

    public bool GetButtonUp(string keyName)
    {
        return Input.GetKeyUp(buttonDictionary[keyName]);
    }

    public int GetAxisRaw(string axisName)
    {
        if(Input.GetKey(axisDictionary[axisName][0]) || Input.GetKeyDown(axisDictionary[axisName][0]))
        {
            return 1;
        }
        else if (Input.GetKey(axisDictionary[axisName][1]) || Input.GetKeyDown(axisDictionary[axisName][0]))
        {
            return -1;
        }
        return 0;
    }
}
