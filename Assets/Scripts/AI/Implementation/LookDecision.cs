using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{

    public override bool Decide(AIManager manager)
    {
        return Look(manager);
    }

    private bool Look(AIManager manager)
    {
        if (!manager.IsTargetAlive())
        {
            return false;
        }
        Vector3 target = manager.GetTargetPosition(); 
        target.y = manager.transform.position.y;
        Vector3 targetDirection = target - manager.transform.position;
        
        float angleToPlayer = Vector3.Angle(targetDirection, manager.transform.forward);

        if ((angleToPlayer >= (manager.aiStats.FOV * -0.5)) && (angleToPlayer <= (manager.aiStats.FOV * 0.5)))
        {   
            RaycastHit hit;
            float lookRange = manager.aiStats.lookRange;

            if(Vector3.Distance(target, manager.transform.position) >= lookRange)
            {
                return false;
            }

            int layerMask = 1 << 9;
            layerMask = ~layerMask;

            if(Physics.Raycast(manager.eyes.transform.position, targetDirection.normalized, out hit, lookRange, layerMask))
            {
                return hit.transform.CompareTag(Constants.PLAYER_TAG);
            }
            //if the player is in the field of view and not occluded by an object return true
        }
        return false;
    }
}