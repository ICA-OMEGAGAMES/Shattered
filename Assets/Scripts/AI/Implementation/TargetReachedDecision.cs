
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Target Reached")]
public class TargetReachedDecision : Decision {

    public override bool Decide(AIManager manager)
    {
        return Reached(manager);
    }

    private bool Reached(AIManager manager)
    {
         return (manager.navMeshAgent.remainingDistance <= manager.movementStats.reachedDistance && !manager.navMeshAgent.pathPending); 
           
    }
}