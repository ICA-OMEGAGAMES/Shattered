using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Yarn.Unity.Shattered;

public class NameSwitcher : MonoBehaviour
{

	public Text talkingName;

    [System.Serializable]
    public struct NameInfo
    {
        public string name;
        public string showingName;
    }
    public NameInfo[] displayingNames;

    [YarnCommand("setName")]
    public void ChangeName(string nameToTalk)
    {
        string nameToSet = null;

        foreach (var info in displayingNames)
        {
            if (info.name == nameToTalk)
            {
				nameToSet = info.showingName;
                break;
            }
        }

        if (nameToSet == null)
        {
            Debug.LogErrorFormat("Can't find sprite named {0}!", nameToTalk);
            return;
        }

		talkingName.text = nameToSet;
    }
}
