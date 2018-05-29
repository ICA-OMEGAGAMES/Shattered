using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Approach Target")]
public class ApproachTarget : Action
{

    public override void Act(AIManager manager)
    {
        Approach(manager);
    }

    private void Approach(AIManager manager)
    {
        Vector3 targetPosition = manager.GetTargetPosition();

        if (!manager.IsCooldownExpired())
        {
            //if the cooldown is not expired yet return 
            return;
        }
        float distance = Vector3.Distance(targetPosition, manager.transform.position);
        if (distance <= (manager.aiStats.movementStats.reachedDistance * manager.aiStats.movementStats.reachedTollerance))
        {
            // if the target is reached stop
            manager.StopMovement();
            return;
        }
        manager.MoveNavMeshAgent(targetPosition, manager.aiStats.movementStats.runSpeed);
    }
}
