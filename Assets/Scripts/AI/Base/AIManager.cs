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

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public AIAnimationManager animationManager;
    [HideInInspector] public Vector3 lastKnownTargetPosition;


    private Transform chaseTarget;
    private MarkerManager markerManager;
    private bool isInCombat = false;
    protected bool characterRooted = true;
    private float attackCooldown;
    private float timer;
    private bool isTimerSet;
    private float health = 100;

    [System.Serializable]
    public class AIStats
    {
        public float moveSpeed = 4f;
        public float strafeSpeed = 5f;
        public float crouchSpeed = 2.0F;
        public float runSpeed = 8.0F;
        public float jumpSpeed = 8.0F;
        public float jumpTime = 0.25f;
        public float jumpCooldown = 1;
        public float dodgeDistance = 10;
        public float dodgeCooldown = 4;
        public float toggleCombatCooldown = 1;
        public float rotateSpeed = 5;
        public float searchTime = 5f;
        
        public float lookSpeed = 1f;
        public float lookRange = 40f;
	    public float FOV = 180f;

        public float reachedDistance = 0.75f;
        public float reachedTollerance = 1.25f;
	    public float attackRange = 7f;
	    public float attackRate = 1f;
	    public float attackForce = 15f;
    	public int attackDamage = 5;
    }

    [SerializeField]
    public AIStats aiStats;

    [Serializable]
    public class UnarmedCombatSettings
    {
        public float lightAttackDuration = 0.5f;
        public float heavyAttackDuration = 0.5f;
        public float cooldown = 3f;
        public bool rootAble = true;
    }
    [SerializeField]
    public UnarmedCombatSettings unarmedCombatSettings;

    public bool SetUpAiManager()
    {
        bool active = false;
        navMeshAgent = GetComponent<NavMeshAgent> ();
        animationManager = GetComponent<AIAnimationManager>();
        GameObject target = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG);
        chaseTarget = target.transform;
        if (chaseTarget != null && wayPointList.Count > 0) 
        {
            active = true;
            navMeshAgent.enabled = true;
        } else 
        {
            navMeshAgent.enabled = false;
        }

        markerManager = this.transform.parent.GetComponent<MarkerManager>();
        markerManager.SetMarkers();

        return active;
    }

    public void SwitchCombatState(bool enabled)
    {
        isInCombat = enabled;
        animationManager.animator.SetBool(animationManager.animations.isInCombat, isInCombat);
    }

     public void SetAttackCooldown(float time)
    {
        attackCooldown = Time.time + time;
    }

    public void ResetAttackCooldown()
    {
        attackCooldown = 0;
    }

    public bool IsCooldownExpired()
    {
        return Time.time > attackCooldown;
    }

    public bool IsCombatEnabled()
    {
        return isInCombat;
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

    public Vector3 GetTargetPosition()
    {
        return new Vector3(chaseTarget.position.x, transform.position.y, chaseTarget.position.z);
    }

    public bool IsNavMeshAgentMoving()
    {
        return !navMeshAgent.isStopped;
    }

    public void MoveNavMeshAgent(Vector3 destination, float speed)
    {
        navMeshAgent.destination = destination;
		navMeshAgent.speed = speed;
		navMeshAgent.isStopped = false;
    }

    public bool IsTimerSet()
    {
        return isTimerSet;
    }

    public void SetTimer(float seconds)
    {
        timer = Time.time + seconds;
        isTimerSet = true;
    }

    public bool IsTimerExpired(){
        bool expired = timer < Time.time;
        if(expired)
        {
            isTimerSet = false;
        }
        return expired && isTimerSet;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            transform.root.gameObject.SetActive(false);
        }
    }
     public void EnableMarkers()
    {
        markerManager.EnableMarkers(aiStats.attackDamage);
    }

    public void DisableMarkers()
    {
        markerManager.DisableMarkers();
    }
}