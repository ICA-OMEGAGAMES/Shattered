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
        Vector3 targetDirection = manager.GetTargetPosition() - manager.transform.position;
        float angleToPlayer = Vector3.Angle(targetDirection, manager.transform.forward);

        if ((angleToPlayer >= (manager.aiStats.FOV * -0.5)) && (angleToPlayer <= (manager.aiStats.FOV * 0.5)))
        {
            RaycastHit hit;
            if(Physics.Raycast(manager.eyes.transform.position, targetDirection.normalized, out hit, manager.aiStats.lookRange))
            {
                float lookRange = manager.aiStats.lookRange - hit.distance;
                while(hit.transform.CompareTag(Constants.ENEMY_TAG))
                {
                    Debug.Log("Other enemy in way");
                    if(!Physics.Raycast(hit.transform.position, targetDirection.normalized, out hit, lookRange) || lookRange <= 0)
                    {
                        return false;
                    }
                    lookRange = manager.aiStats.lookRange - hit.distance;
                }
                return hit.transform.CompareTag(Constants.PLAYER_TAG);
            }
            //if the player is in the field of view and not occluded by an object return true
        }
        return false;
    }
}