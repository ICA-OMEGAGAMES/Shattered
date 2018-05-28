using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision {

    public override bool Decide(AIManager manager)
    {
        return Look(manager);
    }

    private bool Look(AIManager manager)
    {
        if(!manager.IsTargetAlive())
        {
            return false;
        }
        Vector3 targetDirection = manager.GetTargetPosition() - manager.transform.position;
        float angleToPlayer = Vector3.Angle(targetDirection, manager.transform.forward);
        
        if ((angleToPlayer >= (manager.aiStats.FOV * -0.5)) && (angleToPlayer <= (manager.aiStats.FOV * 0.5)))
        {
            RaycastHit hit;

            return (Physics.Raycast(manager.eyes.transform.position, targetDirection.normalized, out hit, manager.aiStats.lookRange) 
                && hit.transform.CompareTag(Constants.PLAYER_TAG));

            //if the player is in the field of view and not occluded by an object return true
        }
        return false;
    }
}