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
            manager.LeaveAttackIdle();
            manager.SetAttackState(false);
            return false;
        }
        Vector3 target = manager.GetTargetPosition(); 
        target.y = manager.transform.position.y;
        Vector3 targetDirection = target - manager.transform.position;
        
        float angleToPlayer = Vector3.Angle(targetDirection, manager.transform.forward);

        if (manager.IsLookingForPlayer() || ((angleToPlayer >= (manager.aiStats.FOV * -0.5)) && (angleToPlayer <= (manager.aiStats.FOV * 0.5))))
        {   
            float lookRange = manager.aiStats.lookRange;

            if(Vector3.Distance(target, manager.transform.position) >= lookRange)
            {
                manager.LeaveAttackIdle();
                manager.SetAttackState(false);
                return false;
            }

            int layerMask =~ LayerMask.GetMask(Constants.AI_LAYER);
            RaycastHit hit;
            if(Physics.Raycast(manager.eyes.transform.position, targetDirection.normalized, out hit, lookRange, layerMask))
            {
                bool player = hit.transform.CompareTag(Constants.PLAYER_TAG);
                
                if(!player)
                {
                    manager.LeaveAttackIdle();
                    manager.SetAttackState(false);
                } else {
                    manager.StopLookingForPlayer();
                    manager.ResetTimerDecision();
                }
               
                return player;
            } 
            //if the player is in the field of view and not occluded by an object return true
        }
        manager.LeaveAttackIdle();
        manager.SetAttackState(false);
        return false;
    }
}