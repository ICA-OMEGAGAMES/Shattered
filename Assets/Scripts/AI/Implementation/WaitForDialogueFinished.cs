using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Yarn.Unity.Shattered;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/WaitForDialogue")]
public class WaitForDialogueFinished : Decision
{
    public override bool Decide(AIManager manager)
    {
        return WaitForDialogue();
    }

    private bool WaitForDialogue()
    {
        return GameObject.FindObjectOfType<YarnCommands>().isHomelessDialogueFinished;
    }
}