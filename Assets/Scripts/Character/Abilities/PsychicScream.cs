using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsychicScream : ISkill
{
    public float maxDistance = 10;
    public float duration = 10;

    private SkillSettings settings;
    private MonoBehaviour mono;

    private float cooldownTimestamp;
    private GameObject[] enemies;
    private List<GameObject> enemiesInRange;

    public PsychicScream(SkillSettings settings, MonoBehaviour mono)
    {
        this.settings = settings;
        this.mono = mono;
    }

    public void Execute(Animator animator)
    {
        if (IsOnCooldown())
        {
            return;
        }

        cooldownTimestamp = Time.time + settings.cooldown;

        if (enemies == null || enemies.Length < 1)
        {
            enemies = GameObject.FindGameObjectsWithTag(Constants.ENEMY_TAG);
        }
        enemiesInRange.Clear();
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(mono.transform.position, enemy.transform.position);
            if (distance <= maxDistance)
            {
                enemiesInRange.Add(enemy);
            }
        }

        enemiesInRange.ForEach(enemy => enemy.GetComponent<AIManager>().PsychicScreamExecuted(duration));
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
