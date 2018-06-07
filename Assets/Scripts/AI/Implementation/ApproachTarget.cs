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
        targetPosition.y = manager.transform.position.y;

        if (!manager.IsCooldownExpired())
        {
            //if the cooldown is not expired yet return 
            return;
        }
        float distance = Vector3.Distance(targetPosition, manager.transform.position);

        if (distance <= (manager.aiStats.movementStats.reachedDistance * (1 + manager.aiStats.movementStats.reachedTollerance)) && 
                distance >= (manager.aiStats.movementStats.reachedDistance))
        {
            Debug.Log("StopExec");
            // if the target is reached stop
            manager.StopMovement();
            return;
        }
        float reachedDistance = manager.aiStats.movementStats.reachedDistance;
        manager.MoveNavMeshAgent(targetPosition - Vector3.Scale((targetPosition - manager.transform.position).normalized, new Vector3(reachedDistance, 1, reachedDistance)), manager.aiStats.movementStats.runSpeed);
    }
}
