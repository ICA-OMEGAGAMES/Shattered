using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManagerPlayer : MarkerManager{

    public override void NotifyHit(GameObject hitTarget, float damage)
    {
        //if the hitTarget is not the ai return
        if (hitTarget.transform.tag != Constants.ENEMY_TAG)
            return;

        if (!hitBySwing.Contains(hitTarget))
        {
            hitBySwing.Add(hitTarget.gameObject);

            string attackMode;
            DevonScript devonScript = GetComponentInChildren<DevonScript>();
            if(devonScript == null || !devonScript.isActiveAndEnabled)
            {   
                MalphasScript malphasScript = GetComponentInChildren<MalphasScript>();
                attackMode = malphasScript.GetAttackMode();
            } else {
                attackMode = devonScript.GetAttackMode();
            }
            AIManager manager =  hitTarget.transform.root.GetComponentInChildren<AIManager>();
            manager.TakeDamage(damage, attackMode);
        }
    }
}
