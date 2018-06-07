﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possess : ISkill
{
    private SkillSettings settings;
    private MonoBehaviour mono;
    private float cooldownTimestamp;
    private MalphasScript.SkillAnimations skillAnimations = new MalphasScript.SkillAnimations();

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
            float shortestDistance = float.MaxValue;
            GameObject closestEnemy = null;
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag(Constants.ENEMY_TAG))
            {
                float distance = (mono.transform.position - enemy.transform.position).sqrMagnitude;
                if(distance < shortestDistance)
                {
                    closestEnemy = enemy;
                    shortestDistance = distance;
                }
            }

            closestEnemy.GetComponent<AIManager>().Possess();
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
