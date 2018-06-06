using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/OutOfRange")]
public class OutOfRangeDecision : Decision
{

    public override bool Decide(AIManager manager)
    {
        return OutOfRange(manager);
    }

    private bool OutOfRange(AIManager manager)
    {
        return Vector3.Distance(manager.transform.position, manager.GetTargetPosition()) >= (2 * manager.aiStats.lookRange);
    }
}