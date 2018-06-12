using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/UnarmedAttack")]
public class UnarmedAttackAction : Action
{

    public override void Act(AIManager manager)
    {
        Attack(manager);
    }

    private void Attack(AIManager manager)
    {
        if (!manager.IsCombatEnabled())
        {
            manager.SwitchCombatState(true);
        }

        if (manager.IsNavMeshAgentMoving())
        {
            //if the ai is moving (approaching) it is out of combat
            return;
        }

        if(manager.IsCooldownExpired() && manager.GeneralCooldownExpired())
        {
            manager.animationManager.SetFightingAnimation(0, Constants.ATTACK1_BUTTON);
            manager.SetAttackCooldown(manager.aiStats.unarmedCombatSettings.cooldown);
            manager.SetGeneralCooldown(manager.aiStats.unarmedCombatSettings.generalCooldown);
        }
    }
}
