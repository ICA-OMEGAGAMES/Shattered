using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManagerAi : MarkerManager
{

    public override void NotifyHit(GameObject hitTarget, float damage)
    {
        //if the hitTarget is not the playe return
        if ((hitTarget.transform.tag != Constants.PLAYER_TAG) && (hitTarget.transform.tag != Constants.ENEMY_TAG))
            return;

        if (!hitBySwing.Contains(hitTarget))
        {
            hitBySwing.Add(hitTarget.gameObject);
            if (hitTarget.transform.tag == Constants.PLAYER_TAG)
            {
                hitTarget.transform.root.GetComponent<Statistics>().ReduceHealth(damage);
                return;
            }

            if (this.transform.root.GetComponentInChildren<AIManager>().IsPossessed())
            {
                //Prevents friendly fire
                string attackMode = GetComponentInChildren<AIManager>().GetAttackMode();
                hitTarget.transform.root.GetComponentInChildren<AIManager>().TakeDamage(damage, attackMode);
            }

        }
    }
}
