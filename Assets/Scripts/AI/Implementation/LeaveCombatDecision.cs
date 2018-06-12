
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Leave Combat")]
public class LeaveCombatDecision : Decision
{

    public override bool Decide(AIManager manager)
    {
        return Leave(manager);
    }

    private bool Leave(AIManager manager)
    {
        if ((Vector3.Distance(manager.transform.position, manager.GetTargetPosition()) > manager.aiStats.unarmedCombatSettings.attackIdleRange)
            || !manager.IsTargetAlive())
        {
            manager.EnterAttackIdle();
            return true;
        }
        
        manager.LeaveAttackIdle();
        return false;
    }
}