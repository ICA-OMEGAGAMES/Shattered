using System;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/AIStats")]
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
        //Not sure yet if its going to be a scriptable object or a class
        public float moveSpeed = 4f;
        public float runSpeed = 8.0F;
        public float dodgeCooldown = 2;
        public float reachedDistance = 0.75f;
        public float reachedTollerance = 1.25f;
    }

    [SerializeField]
    public MovementStats movementStats;

    [Serializable]
    public class UnarmedCombatSettings
    {
        //Not sure yet if its going to be a scriptable object or a class
        public float unarmedAttackRange = 7f;
        public float unarmedAttackDamage = 5f;
        public float lightAttackDuration = 0.5f;
        public float heavyAttackDuration = 0.5f;
        public float cooldown = 2f;
        public float possessionDuration = 10;
    }

    [SerializeField]
    public UnarmedCombatSettings unarmedCombatSettings;
}