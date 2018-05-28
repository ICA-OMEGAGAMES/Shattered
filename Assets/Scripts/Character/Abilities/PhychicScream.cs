using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhychicScream : ISkill
{
    private SkillSettings settings;
    private MonoBehaviour mono;

    private float cooldownTimestamp;

    public PhychicScream(SkillSettings settings, MonoBehaviour mono)
    {
        this.settings = settings;
        this.mono = mono;
    }

    public void Execute(Animator animator)
    {
        if (!IsOnCooldown())
        {
            cooldownTimestamp = Time.time + settings.cooldown;
            float maxDistance = 10;
            List<GameObject> enemiesInRange = new List<GameObject>();
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag(Constants.ENEMY_TAG))
            {
                float distance = Vector3.Distance(mono.transform.position, enemy.transform.position);
                if (distance <= maxDistance)
                {
                    enemiesInRange.Add(enemy);
                }
            }

            enemiesInRange.ForEach(enemy => enemy.GetComponent<AIManager>().PsychicScreamExecuted(10));
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
