using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Flee")]
public class FleeDecision : Decision
{

    public override bool Decide(AIManager manager)
    {
        return Flee(manager);
    }

    private bool Flee(AIManager manager)
    {
        if(manager.IsPsychicScreamAffected())
        {
            manager.GetTargetPosition();
            manager.LeaveAttackIdle();
            return true;
        }
        return false;
    }
}