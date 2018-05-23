﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName="PluggableAI/Actions/Chase")]
public class ChaseAction : Action {

	public override void Act(AIManager manager)
	{
		Chase(manager);
	}

	private void Chase(AIManager manager)
	{
		Vector3 targetPosition = manager.GetTargetPosition();
		manager.lastKnownTargetPosition = targetPosition;
		manager.MoveNavMeshAgent(targetPosition, manager.movementStats.runSpeed);
	}
}
