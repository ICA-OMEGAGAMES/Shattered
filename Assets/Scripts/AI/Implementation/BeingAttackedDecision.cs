using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Being Attacked")]
public class BeingAttackedDecision : Decision
{

    public override bool Decide(AIManager manager)
    {
        return Attacked(manager);
    }

    private bool Attacked(AIManager manager)
    {
        return manager.currentHealth < manager.previousHealth;
    }
}