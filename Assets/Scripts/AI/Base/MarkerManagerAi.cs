using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManagerAi : MarkerManager{

    public override void NotifyHit(GameObject hitTarget, float damage)
    {   
        //if the hitTarget is not the playe return
        if (hitTarget.transform.tag != Constants.PLAYER_TAG)
            return;

        if (!hitBySwing.Contains(hitTarget))
        {
            hitBySwing.Add(hitTarget.gameObject);
            hitTarget.transform.root.GetComponent<Statistics>().ReduceHealth(damage); 
        }
    }
}
