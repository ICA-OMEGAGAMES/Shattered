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
    [HideInInspector] public Vector3 currentTarget;
    [HideInInspector] public Vector3 walkTarget;
    [HideInInspector] public float[] lastVelocities;



    private StateController controller;
    private Transform chaseTarget;
    private MarkerManagerAi markerManager;
    private bool isInCombat = false;
    protected bool characterRooted = true;
    private float attackCooldownTimestamp;
    private float timestamp;
    private bool isTimestampSet;
    private float currentHealth;
    private int currentVelocityIndex;

    [System.Serializable]
    public class AIStats
    {
        //Not sure yet which variables will remain in the end and if its going to be a scriptable object or a class
        public float maxHealth = 100;
        public float toggleCombatCooldown = 1;
        public float searchTime = 5f;

        public float lookSpeed = 1f;
        public float lookRange = 40f;
        public float FOV = 180f;
    }

    [SerializeField]
    public AIStats aiStats;

    [System.Serializable]
    public class MovementStats
    {
        //Not sure yet which variables will remain in the end and if its going to be a scriptable object or a class
        public float moveSpeed = 4f;
        public float strafeSpeed = 5f;
        public float crouchSpeed = 2.0F;
        public float runSpeed = 8.0F;
        public float jumpSpeed = 8.0F;
        public float jumpTime = 0.25f;
        public float jumpCooldown = 1;
        public float dodgeDistance = 10;
        public float dodgeCooldown = 2;
        public float rotateSpeed = 5;
        public float reachedDistance = 0.75f;
        public float reachedTollerance = 1.25f;
        public float attackRange = 7f;
        public float attackRate = 1f;
        public float attackForce = 15f;
        public int attackDamage = 5;
    }

    [SerializeField]
    public MovementStats movementStats;

    [Serializable]
    public class UnarmedCombatSettings
    {
        public float unarmedAttackRange = 7f;
        public float unarmedAttackRate = 1f;
        public float unarmedAttackDamage = 5f;
        public float lightAttackDuration = 0.5f;
        public float heavyAttackDuration = 0.5f;
        public float cooldown = 3f;
        public bool rootAble = true;
    }
    [SerializeField]
    public UnarmedCombatSettings unarmedCombatSettings;

    public bool SetUpAiManager(StateController controller)
    {
        bool active = false;
        lastVelocities = new float[30];
        this.controller = controller;
        currentHealth = aiStats.maxHealth;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animationManager = GetComponent<AIAnimationManager>();
        FindChaseTarget();
        navMeshAgent.enabled = true;

        markerManager = this.transform.parent.GetComponent<MarkerManagerAi>();
        markerManager.SetMarkers();

        active = true;
        return active;
    }

    void Update()
    {
        if (chaseTarget == null || !chaseTarget.gameObject.activeSelf)
        {
            FindChaseTarget();
        }
    }

    void FindChaseTarget()
    {
        Array.ForEach(GameObject.FindGameObjectsWithTag(Constants.PLAYER_TAG), element =>
        {
            if (element.gameObject.activeSelf)
            {
                chaseTarget = element.transform;
            }
            chaseTarget = element.transform;
        });
        if (chaseTarget == null)
        {
            Debug.LogError("Player not found.");
        }
    }

    public void SwitchCombatState(bool enabled)
    {
        isInCombat = enabled;
        animationManager.animator.SetBool(animationManager.animations.isInCombat, isInCombat);
    }

    public void SetAttackCooldown(float time)
    {
        attackCooldownTimestamp = Time.time + time;
    }

    public void ResetAttackCooldown()
    {
        attackCooldownTimestamp = 0;
    }

    public bool IsCooldownExpired()
    {
        return Time.time > attackCooldownTimestamp;
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
        if (chaseTarget == null)
        {
            Debug.LogError("Target not found.");
            return transform.position;
        }
        return new Vector3(chaseTarget.position.x, transform.position.y, chaseTarget.position.z);
    }

    public bool IsNavMeshAgentMoving()
    {
        return !navMeshAgent.isStopped;
    }

    public void MoveNavMeshAgent(Vector3 destination, float speed)
    {
        walkTarget = destination;
        lastVelocities[currentVelocityIndex++ % (lastVelocities.Length - 1)] = navMeshAgent.velocity.magnitude;
        NavMeshPath path = new NavMeshPath();
        if (!navMeshAgent.CalculatePath(destination, path) || path.status == NavMeshPathStatus.PathPartial)
        {   
            destination = transform.position + (destination - transform.position).normalized;

            currentTarget = destination;
            navMeshAgent.destination = destination;
            navMeshAgent.speed = speed;
            navMeshAgent.isStopped = false;
            return;
        }
        currentTarget = walkTarget;
        navMeshAgent.destination = destination;
        navMeshAgent.speed = speed;
        navMeshAgent.isStopped = false;
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
        bool expired = timestamp < Time.time;
        if (expired)
        {
            isTimestampSet = false;
        }
        return expired && isTimestampSet;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            StopMovement();
            SwitchCombatState(false);
            controller.Die();
            animationManager.Die();
        }
    }
    public void EnableMarkers()
    {
        markerManager.EnableMarkers(unarmedCombatSettings.unarmedAttackDamage);
    }

    public void DisableMarkers()
    {
        markerManager.DisableMarkers();
    }
}