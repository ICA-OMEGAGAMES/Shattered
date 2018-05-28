using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Switch To Attack")]
public class SwitchToAttackDecision : Decision
{

    public override bool Decide(AIManager manager)
    {
        return SwitchToAttack(manager);
    }

    private bool SwitchToAttack(AIManager manager)
    {
        if ((manager.GetAttackingAis() < 2) && manager.IsAttackModeCooldownExpired())
        {
            return true;
        }
        return false;
    }
}