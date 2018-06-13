using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Turn")]
public class TurnAction : Action
{
    public float duration = 5f;
    public override void Act(AIManager manager)
    {
        Turn(manager);
    }

    private void Turn(AIManager manager)
    {
        if(!manager.IsLookingForPlayer())
        {
            manager.LookForPlayer(duration);
        }
    }
}