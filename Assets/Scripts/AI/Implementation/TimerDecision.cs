using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Timer")]
public class TimerDecision : Decision {

    public override bool Decide(AIManager manager)
    {
        return CheckTimer(manager);
    }

    private bool CheckTimer(AIManager manager)
    {
        if(!manager.IsTimestampSet())
        {
            manager.SetTimestamp(manager.aiStats.searchTime);
        }
        return manager.IsTimestampExpired();
    }
}