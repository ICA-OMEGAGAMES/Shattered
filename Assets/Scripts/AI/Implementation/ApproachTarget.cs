using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName="PluggableAI/Actions/Approach Target")]
public class ApproachTarget : Action {

	public override void Act(AIManager manager)
	{
		Approach(manager);
	}

	private void Approach(AIManager manager)
	{
		 Vector3 targetPosition = manager.GetTargetPosition();

        if(!manager.IsCooldownExpired() || Vector3.Distance(targetPosition, manager.transform.position) <= (manager.aiStats.reachedDistance * manager.aiStats.reachedTollerance)){
            manager.StopMovement();
            return;
        }

        manager.SwitchCombatState(false);
		manager.MoveNavMeshAgent(targetPosition, manager.aiStats.moveSpeed);
	}
}
