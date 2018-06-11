using System;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/AIStats")]
public class AIStats : ScriptableObject
{

    public float maxHealth = 100;
    public float searchTime = 5f;
    public float lookSpeed = 1f;
    public float lookRange = 40f;
    public float FOV = 180f;

    [System.Serializable]
    public class MovementStats
    {
        public float moveSpeed = 4f;
        public float runSpeed = 8.0F;
        public float dodgeCooldown = 2;
        public float reachedDistance = 0.75f;
        public float reachedTollerance = 0.25f;
    }

    [SerializeField]
    public MovementStats movementStats;

    [Serializable]
    public class UnarmedCombatSettings
    {
        public float blockDuration = 3f;
        public float blockPercentage = 0.5f;
        public float attackIdleRange = 8f;
        public float unarmedAttackRange = 5f;
        public float unarmedAttackDamage = 5f;
        public float lightAttackDuration = 0.5f;
        public float heavyAttackDuration = 0.5f;
        public float cooldown = 2f;
        public float possessionDuration = 10f;
        public float attackingDuration = 7.5f;
        public float attackModeCooldown = 3.75f;
        public float stunDuration = 1f;
        public float immunityAgainstPunch;
        public float immunityAgainstKick;
        public float immunityAgainstWeapons;
    }

    [SerializeField]
    public UnarmedCombatSettings unarmedCombatSettings;
}