using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Target Reached")]
public class TargetReachedDecision : Decision {

    public override bool Decide(AIManager manager)
    {
        return Reached(manager);
    }

    private bool Reached(AIManager manager)
    {
         return (Vector3.Distance(manager.transform.position, manager.walkTarget) <= manager.aiStats.movementStats.reachedDistance && !manager.navMeshAgent.pathPending);
    }
}