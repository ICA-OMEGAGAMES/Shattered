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

            //TODO: Dummyscript is just for demo so the if can be removed later on
            Component component = hitTarget.transform.root.GetComponentInChildren<AIManager>();
            if(component == null)
            {
                component = hitTarget.transform.root.GetComponent<DummyScript>();
                ((DummyScript) component).TakeDamage(damage);
                return;
            }
            string attackMode;
            DevonScript devonScript = GetComponentInChildren<DevonScript>();
            if(devonScript == null || !devonScript.isActiveAndEnabled)
            {   
                MalphasScript malphasScript = GetComponentInChildren<MalphasScript>();
                attackMode = malphasScript.GetAttackMode();
            } else {
                attackMode = devonScript.GetAttackMode();
            }
            ((AIManager) component).TakeDamage(damage, attackMode);
        }
    }
}
