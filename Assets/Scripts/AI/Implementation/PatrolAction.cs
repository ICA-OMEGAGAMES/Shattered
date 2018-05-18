using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(AIManager manager)
    {
        Patrol (manager);
    }

    private void Patrol(AIManager manager)
    {   
        manager.MoveNavMeshAgent(manager.wayPointList [manager.nextWayPoint].position, manager.aiStats.moveSpeed);

        if (manager.navMeshAgent.remainingDistance <= manager.aiStats.reachedDistance && !manager.navMeshAgent.pathPending) 
        {
            manager.nextWayPoint++;
            manager.nextWayPoint = manager.nextWayPoint % manager.wayPointList.Count;
        }
    }
}