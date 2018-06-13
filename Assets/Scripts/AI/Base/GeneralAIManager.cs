using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GeneralAIManager : MonoBehaviour
{
    private float attackCooldownTimestamp;
    private List<Transform> waitingAIs;
    private List<Transform> attackingAIs;

    void Start()
    {
        waitingAIs = new List<Transform>();
        attackingAIs = new List<Transform>();
    }

    public void AttackState(bool enabled, Transform enemy)
    {
        if (enabled && !attackingAIs.Contains(enemy))
        {
            attackingAIs.Add(enemy);
            return;
        } else if (!enabled)
        {
            attackingAIs.Remove(enemy);
        }
    }

    public int GetAttackingAIS()
    {
        return attackingAIs.Count;
    }

    public void SetCooldown(float duration)
    {
        attackCooldownTimestamp = Time.time + duration;
    }

    public bool IsCooldownExpired()
    {
        return Time.time > attackCooldownTimestamp;
    }

    public void EnterAttackIdle(Transform enemy)
    {
       if(!waitingAIs.Contains(enemy))
       {
           waitingAIs.Add(enemy);
       }
    }

    public void LeaveAttackIdle(Transform enemy)
    {
        waitingAIs.Remove(enemy);
    }

    public int GetWaitingAIs()
    {
        return waitingAIs.Count;
    }
}