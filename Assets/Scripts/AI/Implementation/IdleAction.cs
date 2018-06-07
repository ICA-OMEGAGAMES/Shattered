using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Idle")]
public class IdleAction : Action
{

    public override void Act(AIManager manager)
    {
        Idle(manager);
    }

    private void Idle(AIManager manager)
    {
        //Todo Play idle animation
    }
}
