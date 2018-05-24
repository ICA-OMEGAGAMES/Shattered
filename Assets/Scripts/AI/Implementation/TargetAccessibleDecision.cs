using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Target Accessible")]
public class TargetAccessibleDecision : Decision {

    public override bool Decide(AIManager manager)
    {
        if(Accessible(manager))
        {
        return true;
        }
        Debug.Log("Not reachable and reached");
        return false;
    }

    private bool Accessible(AIManager manager)
    {
         return !((manager.walkTarget != manager.currentTarget) && manager.lastVelocities.Sum() == 0 && !manager.navMeshAgent.pathPending);
    }
}