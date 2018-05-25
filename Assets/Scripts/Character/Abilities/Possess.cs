using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possess : MonoBehaviour, ISkill
{
    SkillSettings settings;
    private float cooldownTimestamp;

    public Possess(SkillSettings settings)
    {
        this.settings = settings;
    }

    public void Execute(Animator animator)
    {
        if (!IsOnCooldown())
        {
            cooldownTimestamp = Time.time + settings.cooldown;
            print("Possess Used");
            float shortestDistance = float.MaxValue;
            GameObject closestEnemy = null;
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag(Constants.ENEMY_TAG))
            {
                float distance = (transform.position - enemy.transform.position).sqrMagnitude;
                if(distance < shortestDistance)
                {
                    closestEnemy = enemy;
                    shortestDistance = distance;
                }
            }

            closestEnemy.GetComponent<AIManager>().Posess();
        }
    }

    public bool IsOnCooldown()
    {
        if (cooldownTimestamp > Time.time)
        {
            return true;
        }
        return false;
    }
}
