using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Target Accessible")]
public class TargetAccessibleDecision : Decision {

    public override bool Decide(AIManager manager)
    {   
        return Accessible(manager);
    }

    private bool Accessible(AIManager manager)
    {   
        if(manager.navMeshAgent.isStopped)
        {
            manager.RefreshTarget();
        }
        return (manager.TargetAccessible() || manager.framesWithoutMovement < 10);
    }
}