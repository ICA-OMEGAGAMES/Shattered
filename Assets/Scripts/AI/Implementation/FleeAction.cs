using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Flee")]
public class FleeAction : Action
{

    public override void Act(AIManager manager)
    {
        Flee(manager);
    }

    private void Flee(AIManager manager)
    {
        Vector3 targetPosition = manager.transform.position + (manager.transform.position - manager.GetTargetPosition()).normalized;
        manager.MoveNavMeshAgent(targetPosition, manager.aiStats.movementStats.runSpeed);
    }
}
