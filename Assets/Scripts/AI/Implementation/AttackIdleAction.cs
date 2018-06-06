
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/AttackIdle")]
public class AttackIdleAction : Action
{
    public override void Act(AIManager manager)
    {
        AttackIdle(manager);
    }

    private void AttackIdle(AIManager manager)
    {
        if (Vector3.Distance(manager.GetTargetPosition(), manager.transform.position) < manager.aiStats.unarmedCombatSettings.unarmedAttackRange
            || Vector3.Distance(manager.GetTargetPosition(), manager.transform.position) > manager.aiStats.unarmedCombatSettings.attackIdleRange)
        {
            Vector3 direction = manager.GetTargetPosition() +  ((manager.transform.position - manager.GetTargetPosition()).normalized * Random.Range(manager.aiStats.unarmedCombatSettings.unarmedAttackRange, manager.aiStats.unarmedCombatSettings.attackIdleRange));

            manager.MoveNavMeshAgent(direction, manager.aiStats.movementStats.runSpeed);
        }
        else
        {
            manager.StopMovement();
        }
    }
}