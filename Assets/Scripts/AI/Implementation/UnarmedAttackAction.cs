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

        if (manager.IsCooldownExpired() && !manager.IsAttackTimestampSet())
        {
            manager.SetAttackTimestamp(0.75f);
            Debug.Log("Tease");
        }

        if(manager.IsAttackTimestampExpired())
        {
            float random = UnityEngine.Random.Range(0.0f, 5.0f);
            //kick or punch based on chance
            if (random < 4)
            {
                manager.animationManager.SetFightingAnimation(0, 1);
            }
            else
            {
                manager.animationManager.SetFightingAnimation(0, 2);
            }
            manager.SetAttackCooldown(manager.aiStats.unarmedCombatSettings.cooldown);
        }
    }
}
