using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AIAnimationManager))]
public class GeneralAIManager : MonoBehaviour
{
    private int attackingAIs;

    public void AttackState(bool enabled)
    {
        if (enabled)
        {
            attackingAIs++;
            return;
        }
        attackingAIs--;
    }

    public int GetAttackingAIS()
    {
        return attackingAIs;
    }
}