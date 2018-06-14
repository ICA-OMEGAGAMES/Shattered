using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/WaitForDialogue")]
public class WaitForDialogueFinished : Decision
{
    public override bool Decide(AIManager manager)
    {
        return WaitForDialogue();
    }

    private bool WaitForDialogue()
    {
        return false;
    }
}