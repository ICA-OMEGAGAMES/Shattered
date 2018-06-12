
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Target Alive")]
public class TargetAliveDecision : Decision
{

    public override bool Decide(AIManager manager)
    {
        return IsAlive(manager);
    }

    private bool IsAlive(AIManager manager)
    {
        if(!manager.IsTargetAlive())
        {
            manager.EnterAttackIdle();
            manager.SetAttackState(false);
            return false;
        }
        return true;
    }
}