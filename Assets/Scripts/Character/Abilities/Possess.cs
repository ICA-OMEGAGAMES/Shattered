using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possess : ISkill
{
    private SkillSettings settings;
    private MonoBehaviour mono;
    private float cooldownTimestamp;

    public Possess(SkillSettings settings, MonoBehaviour mono)
    {
        this.settings = settings;
        this.mono = mono;
    }

    public void Execute(Animator animator)
    {
        if (!IsOnCooldown())
        {
            cooldownTimestamp = Time.time + settings.cooldown;
<<<<<<< HEAD
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
=======
            //("Possess Used");
>>>>>>> origin/development_programming
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
