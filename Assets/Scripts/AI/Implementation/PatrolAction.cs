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
        if(manager.wayPointList.Count <= 0)
        {
            Debug.LogError("No waypoints specified!");
            return;
        }
        manager.MoveNavMeshAgent(manager.wayPointList [manager.nextWayPoint].position, manager.aiStats.movementStats.moveSpeed);

        if (Vector3.Distance(manager.transform.position, manager.walkTarget) <= manager.aiStats.movementStats.reachedDistance && !manager.navMeshAgent.pathPending) 
        {
            manager.nextWayPoint++;
            manager.nextWayPoint = manager.nextWayPoint % manager.wayPointList.Count;
        }
    }
}