using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Turn")]
public class TurnAction : Action
{
    public override void Act(AIManager manager)
    {
        Turn (manager);
    }

    private void Turn(AIManager manager)
    {
        manager.transform.Rotate(new Vector3(0,3,0));
    }
}