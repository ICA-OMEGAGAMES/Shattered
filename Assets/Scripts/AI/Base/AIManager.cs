using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AIAnimationManager))]
public class AIManager : MonoBehaviour
{
    public List<Transform> wayPointList;
    public GameObject eyes;
    public AIStats aiStats;

   
    public Vector3 lastKnownTargetPosition { get; set; }
    public int nextWayPoint { get; set; }
    public NavMeshAgent navMeshAgent { get; private set; }
    public AIAnimationManager animationManager { get; private set; }
    public Vector3 walkTarget { get; private set; }
    public int framesWithoutMovement { get; private set; }
    public float currentHealth { get; private set; }
    public float previousHealth { get; private set; }


    private StateController controller;
    private GeneralAIManager generalAiManager;
    private Transform currentChaseTarget;
    private Transform defaultChaseTarget;
    private MarkerManagerAi markerManager;
    private bool isInCombat = false;
    private float attackCooldownTimestamp;
    private float timestamp;
    private bool isTimestampSet;
    private float attackTimestamp;
    private bool attackTimestampSet;
    private float possessedTimestamp;
    private float psychicScreamTimestamp;
    private float attackDurationTimestamp;
    private float attackModeCooldownTimestamp;

    public bool SetUpAiManager(StateController controller)
    {
        bool active = false;
        this.controller = controller;
        currentHealth = aiStats.maxHealth;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animationManager = GetComponent<AIAnimationManager>();
        generalAiManager = GameObject.FindObjectOfType<GeneralAIManager>();
        FindChaseTarget();
        defaultChaseTarget = currentChaseTarget;
        navMeshAgent.enabled = true;

        markerManager = this.transform.parent.GetComponent<MarkerManagerAi>();
        markerManager.SetMarkers();

        active = true;
        return active;
    }

    void Update()
    {
        CheckMovement();
        if (currentChaseTarget == null || !currentChaseTarget.gameObject.activeSelf)
        {
            FindChaseTarget();
        }

        if (possessedTimestamp <= Time.time)
        {
            currentChaseTarget = defaultChaseTarget;
        }
        else if (possessedTimestamp > Time.time && !IsTargetAlive())
        {
            currentChaseTarget = SelectClosestEnemy().transform;
        }
    }

    void FixedUpdate()
    {
        previousHealth = currentHealth;
    }

    void FindChaseTarget()
    {
        Array.ForEach(GameObject.FindGameObjectsWithTag(Constants.PLAYER_TAG), (Action<GameObject>)(element =>
        {
            if (element.gameObject.activeSelf)
            {
                currentChaseTarget = element.transform;
                defaultChaseTarget = currentChaseTarget;
            }
        }));
        if (currentChaseTarget == null || !currentChaseTarget.gameObject.activeSelf)
        {
            Debug.LogError("Player not found.");
        }
    }

    public void SwitchCombatState(bool enabled)
    {
        isInCombat = enabled;
        animationManager.animator.SetBool(animationManager.animations.isInCombat, isInCombat);
        if (enabled)
        {
            attackDurationTimestamp = Time.time + aiStats.unarmedCombatSettings.attackingDuration;
        }
        else
        {
            attackModeCooldownTimestamp = Time.time + aiStats.unarmedCombatSettings.attackModeCooldown;
        }
    }

    public void SetAttackCooldown(float time)
    {
        attackCooldownTimestamp = Time.time + time;
    }

    public bool IsCooldownExpired()
    {
        return Time.time > attackCooldownTimestamp;
    }

    public bool IsCombatEnabled()
    {
        return isInCombat;
    }

    public int GetAttackingAis()
    {
        return generalAiManager.GetAttackingAIS();
    }

    public void SetAttackState(bool active)
    {
        generalAiManager.AttackState(active, transform);
    }

    public bool GeneralCooldownExpired()
    {
        return generalAiManager.IsCooldownExpired();
    }

    public void SetGeneralCooldown(float time)
    {
        generalAiManager.SetCooldown(time);
    }

    public void EnterAttackIdle()
    {
        generalAiManager.EnterAttackIdle(transform.parent);
    }

    public void LeaveAttackIdle()
    {
        generalAiManager.LeaveAttackIdle(transform.parent);
    }

    public int GetWaitingAIs()
    {
        return generalAiManager.GetWaitingAIs();
    }

    public bool IsAttackModeCooldownExpired()
    {
        return attackModeCooldownTimestamp < Time.time;
    }

    public bool IsAttackDurationOver()
    {
        return attackDurationTimestamp < Time.time;
    }

    public void StopMovement()
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.speed = 0f;
        navMeshAgent.isStopped = true;
    }

    public bool Dodge(bool dodge)
    {
        return animationManager.Dodge(dodge);
    }

    public bool Block(bool block, float duration)
    {
        return animationManager.Block(block, duration);
    }

    public Vector3 GetTargetPosition()
    {
        if (currentChaseTarget == null)
        {
            Debug.LogError("Target not found.");
            return transform.position;
        }
        return new Vector3(currentChaseTarget.position.x, transform.position.y, currentChaseTarget.position.z);
    }

    public bool IsNavMeshAgentMoving()
    {
        return !navMeshAgent.isStopped;
    }

    public void MoveNavMeshAgent(Vector3 destination, float speed)
    {
        walkTarget = destination;
        if (!TargetAccessible())
        {
            destination = transform.position + (destination - transform.position).normalized;

            navMeshAgent.destination = destination;
            navMeshAgent.speed = speed;
            navMeshAgent.isStopped = false;
            return;
        }

        navMeshAgent.destination = destination;
        navMeshAgent.speed = speed;
        navMeshAgent.isStopped = false;
    }

    public bool TargetAccessible()
    {
        NavMeshPath path = new NavMeshPath();
        return navMeshAgent.CalculatePath(walkTarget, path) && !(path.status == NavMeshPathStatus.PathPartial);
    }

    private void CheckMovement()
    {
        if (navMeshAgent.velocity.sqrMagnitude < 1)
        {
            framesWithoutMovement++;
            return;
        }
        else
        {
            framesWithoutMovement = 0;
        }
    }

    public void RefreshTarget()
    {

        if (controller.previousState != null && (controller.previousState.name == "Chase" || controller.previousState.name == "PlayerLost"))
        {
            walkTarget = currentChaseTarget.transform.position;
            return;
        }
        walkTarget = wayPointList[nextWayPoint].position;
    }

    public bool IsTimestampSet()
    {
        return isTimestampSet;
    }

    public void SetTimestamp(float seconds)
    {
        timestamp = Time.time + seconds;
        isTimestampSet = true;
    }

    public bool IsTimestampExpired()
    {
        bool expired = (timestamp < Time.time) && isTimestampSet;
        if (expired)
        {
            isTimestampSet = false;
        }
        return expired;
    }

    public void TakeDamage(float amount, string attack)
    {
        switch(attack)
        {
            case Constants.PUNCH_ATTACK:
                amount = amount - (amount * aiStats.unarmedCombatSettings.immunityAgainstPunch);
                break;
            case Constants.KICK_ATTACK:
                amount = amount - (amount * aiStats.unarmedCombatSettings.immunityAgainstKick);
                break;
            case Constants.WEAPON_ATTACK:
                amount = amount - (amount * aiStats.unarmedCombatSettings.immunityAgainstWeapons);
                break;
        }

        previousHealth = currentHealth;
        if(animationManager.IsBlocking())
        {
            amount =  (amount * aiStats.unarmedCombatSettings.blockPercentage);
        } else {
            animationManager.HitRegistered();
        }
        
        currentHealth -=  amount;
        attackCooldownTimestamp = Time.time + aiStats.unarmedCombatSettings.stunDuration;
        if (currentHealth <= 0)
        {
            StopMovement();
            if (isInCombat)
            {
                SwitchCombatState(false);
            }
            controller.Die();
            animationManager.Die();
            LeaveAttackIdle();
        }
    }

    public void EnableMarkers()
    {   
        if(!IsPossessed())
        {
            markerManager.EnableMarkers(aiStats.unarmedCombatSettings.unarmedAttackDamage);
            return;
        }
        markerManager.EnableMarkers(100);
    }

    public void DisableMarkers()
    {
        markerManager.DisableMarkers();
    }

    public void ResetAttackTimer()
    {
        attackTimestampSet = false;
    }

    public bool IsAttackTimestampSet()
    {
        return attackTimestampSet;
    }

    public void Possess()
    {
        if (possessedTimestamp >= Time.time)
        {
            //already possessed so extend possession
            possessedTimestamp += aiStats.unarmedCombatSettings.possessionDuration;
            return;
        }


        GameObject closestEnemy = SelectClosestEnemy();

        if (Vector3.Distance(closestEnemy.transform.position, transform.position) > aiStats.lookRange)
        {
            //if no enemy is in range return
            return;
        }

        //set the enemy as the new target and the timestamp
        defaultChaseTarget = currentChaseTarget;
        currentChaseTarget = closestEnemy.transform;
        possessedTimestamp = Time.time + aiStats.unarmedCombatSettings.possessionDuration;
    }

    private GameObject SelectClosestEnemy()
    {
        //search for closest enemy
        float shortestDistance = float.MaxValue;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag(Constants.ENEMY_TAG))
        {
            float distance = (transform.position - enemy.transform.position).sqrMagnitude;
            if (distance < shortestDistance && enemy != this.gameObject && enemy.activeSelf)
            {
                closestEnemy = enemy;
                shortestDistance = distance;
            }
        }
        return closestEnemy;
    }

    public bool IsPossessed()
    {
        return possessedTimestamp >= Time.time;
    }

    public void PsychicScreamExecuted(float duration)
    {
        psychicScreamTimestamp = Time.time + duration;
    }

    public bool IsPsychicScreamAffected()
    {
        return psychicScreamTimestamp > Time.time;
    }

    public bool IsTargetAlive()
    {
        Statistics statistics = currentChaseTarget.transform.root.GetComponentInChildren<Statistics>();

        if (statistics == null)
        {
            return currentChaseTarget.transform.root.GetComponentInChildren<AIManager>().currentHealth > 0;
        }

        return statistics.GetHealth() > 0;
    }

    public string GetAttackMode()
    {
        return animationManager.GetLastAttack();
    }

    public void LookForPlayer(float duration)
    {
        animationManager.LookAround(duration);
    }

    public void StopLookingForPlayer()
    {
        animationManager.StopLooking();
    }

    public bool IsLookingForPlayer()
    {
        return animationManager.IsLooking();
    }

    public void ResetTimerDecision()
    {
        isTimestampSet = false;
    }
}