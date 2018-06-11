
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Approach Last Known Position")]
public class ApproachLastKnownPosition : Action
{
    public override void Act(AIManager manager)
    {
        Approach(manager);
    }

    private void Approach(AIManager manager)
    {
        manager.MoveNavMeshAgent(manager.lastKnownTargetPosition, manager.aiStats.movementStats.moveSpeed);
    }
}