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
        if(manager.IsNavMeshAgentMoving())
        {
            return;
        }

        if (!manager.IsCombatEnabled())
        {
            manager.SwitchCombatState(true);
            manager.ResetAttackCooldown();
        }
        
        if (manager.IsCooldownExpired())
        {           
            float random = UnityEngine.Random.Range(0.0f, 5.0f);

            if(random < 4 )
            {
                manager.animationManager.SetFightingAnimation(0, 1);
            } else {
                manager.animationManager.SetFightingAnimation(0, 2);
            }

            manager.SetAttackCooldown(manager.unarmedCombatSettings.cooldown);
        }
    }
}
