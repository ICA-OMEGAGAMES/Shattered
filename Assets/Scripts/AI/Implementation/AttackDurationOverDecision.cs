using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Attack Duration Over")]
public class AttackDurationOver : Decision
{

    public override bool Decide(AIManager manager)
    {
        return DurationOver(manager);
    }

    private bool DurationOver(AIManager manager)
    {
        if(manager.GetWaitingAIs() < 1)
        {
            return false;
        }
        if(manager.IsAttackDurationOver())
        {
            manager.ResetAttackTimer();
            manager.SetAttackState(false);
            manager.EnterAttackIdle();
            return true;
        }
        return false;

    }
}