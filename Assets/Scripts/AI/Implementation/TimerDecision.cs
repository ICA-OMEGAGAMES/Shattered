using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Timer")]
public class TimerDecision : Decision {
    public float Seconds;

    public override bool Decide(AIManager manager)
    {
        return CheckTimer(manager);
    }

    private bool CheckTimer(AIManager manager)
    {
        if(!manager.IsTimerSet())
        {
            manager.SetTimer(Seconds);
        }
        return manager.IsTimerExpired();
    }
}