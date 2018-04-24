using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsSettings
{
    public Dictionary<string, KeyCode> controlDictionary { get; private set; }

    private static ControlsSettings controlsSettings;

    private ControlsSettings() { }

    public bool Initialized { get; private set; }

    public static ControlsSettings Instance
    {
        get
        {
            if (controlsSettings == null)
            {
                controlsSettings = new ControlsSettings();
            }
            return controlsSettings;
        }
    }
    public void Initialize(ControlButton[] controlButtons)
    {
        if(controlButtons.Length < 1)
        {
            Debug.Log("No controls specified.");
            return;
        }
        controlDictionary = new Dictionary<string, KeyCode>();
        foreach (ControlButton button in controlButtons)
        {
            try
            {
                controlDictionary.Add(button.name, (KeyCode)System.Enum.Parse(typeof(KeyCode), button.key, true));
            }
            catch (ArgumentException argEx)
            {
                Debug.LogError("Button " + button.name + ": Key " + button.key + " not found." + argEx.StackTrace);
            }
        }
        Initialized = true;
    }

    public void Add(KeyValuePair<string, KeyCode> controlButton)
    {
        if(controlDictionary == null)
        {
            Initialized = true;
            controlDictionary = new Dictionary<string, KeyCode>();
        }
        controlDictionary.Add(controlButton.Key, controlButton.Value);
    }

    public void SetKeyMap(string keyName, KeyCode key)
    {
        if (!controlDictionary.ContainsKey(keyName))
        {
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyName);
        }
        controlDictionary[keyName] = key;
    }
 
    public bool GetButtonDown(string keyName)
    {
        return Input.GetKeyDown(controlDictionary[keyName]);
    }

    public bool GetButton(string keyName)
    {
        return Input.GetKey(controlDictionary[keyName]);
    }

    public bool GetButtonUp(string keyName)
    {
        return Input.GetKeyUp(controlDictionary[keyName]);
    }
}
