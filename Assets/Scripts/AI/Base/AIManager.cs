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
    [HideInInspector] public Vector3 walkTarget;
    [HideInInspector] public int framesWithoutMovement;
    [HideInInspector] public float previousHealth;


    private StateController controller;
    private Transform currentChaseTarget;
    private Transform defaultChaseTarget;
    private MarkerManagerAi markerManager;
    private bool isInCombat = false;
    protected bool characterRooted = true;
    private float attackCooldownTimestamp;
    private float timestamp;
    private bool isTimestampSet;
    private int currentVelocityIndex;
    private float possessedTimestamp;
    private bool possessed;

    [System.Serializable]
    public class AIStats
    {
        //Not sure yet which variables will remain in the end and if its going to be a scriptable object or a class
        public float maxHealth = 100;
        public float currentHealth;
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
        public float possessionDuration = 10;
    }
    [SerializeField]
    public UnarmedCombatSettings unarmedCombatSettings;

    public bool SetUpAiManager(StateController controller)
    {
        bool active = false;
        this.controller = controller;
        aiStats.currentHealth = aiStats.maxHealth;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animationManager = GetComponent<AIAnimationManager>();
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

        if (possessedTimestamp <= Time.time && possessed)
        {
            possessed = false;
            currentChaseTarget = defaultChaseTarget;
        }
    }

    void FixedUpdate()
    {
        previousHealth = aiStats.currentHealth;
    }

    void FindChaseTarget()
    {
        Array.ForEach(GameObject.FindGameObjectsWithTag(Constants.PLAYER_TAG), (Action<GameObject>)(element =>
        {
            if (element.gameObject.activeSelf)
            {
                this.currentChaseTarget = element.transform;
            }
            this.currentChaseTarget = element.transform;
        }));
        if (currentChaseTarget == null)
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

    public void TakeDamage(float amount)
    {
        previousHealth = aiStats.currentHealth;
        aiStats.currentHealth -= amount;
        if (aiStats.currentHealth <= 0)
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

    public void Possess()
    {
        if (possessed)
        {
            //already possessed so extend possession
            possessedTimestamp += unarmedCombatSettings.possessionDuration;
            return;
        }


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

        if(Vector3.Distance(closestEnemy.transform.position, transform.position) > aiStats.lookRange)
        {
            //if no enemy is in range return
            Debug.Log("No other enemy in range");
            return;
        }

        //set the enemy as the new target and the timestamp
        defaultChaseTarget = currentChaseTarget;
        currentChaseTarget = closestEnemy.transform;
        possessedTimestamp = Time.time + unarmedCombatSettings.possessionDuration;
        possessed = true;
    }

    public bool IsPossessed()
    {
        return possessed;
    } 
}