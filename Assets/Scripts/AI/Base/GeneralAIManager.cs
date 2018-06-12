using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GeneralAIManager : MonoBehaviour
{
    private int attackingAIs;
    private float attackCooldownTimestamp;
    private List<Transform> waitingAIs;

    void Start()
    {
        waitingAIs = new List<Transform>();
    }

    public void AttackState(bool enabled)
    {
        if (enabled)
        {
            Debug.Log("EnableCalled");
            attackingAIs++;
            return;
        }
         Debug.Log("DisableCalled");
        attackingAIs--;
    }

    public int GetAttackingAIS()
    {
        return attackingAIs;
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
           Debug.Log("EnableIdle");
           waitingAIs.Add(enemy);
       }
    }

    public void LeaveAttackIdle(Transform enemy)
    {
        
        bool test = waitingAIs.Remove(enemy);

        if(test)
        {
            Debug.Log("DisbleIdle");
        }
    }

    public int GetWaitingAIs()
    {
        if(waitingAIs.Count > 0)
        {
            Debug.Log("Waiting AIs: " + waitingAIs.Count);
        }
        return waitingAIs.Count;
    }
}