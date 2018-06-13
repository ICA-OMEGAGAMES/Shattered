using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : ISkill
{
    private SkillSettings settings;
    private Statistics statistics;
    private MonoBehaviour mono;

    private float cooldownTimestamp;

    public Barrier(SkillSettings settings, Statistics statistics, MalphasScript mono)
    {
        this.settings = settings;
        this.statistics = statistics;
        this.mono = mono;
    }

    public void Execute(Animator animator)
    {
        if (!IsOnCooldown())
        {
            cooldownTimestamp = Time.time + settings.cooldown;
            mono.StartCoroutine(ActivateShield());

            mono.StartCoroutine(mono.GetComponent<MalphasScript>().RootCharacter(settings.rootDuration));
        }
    }
    
    IEnumerator ActivateShield()
    {
        statistics.SetBlocks(settings.value);
        yield return new WaitForSeconds(settings.duration);
        mono.StopCoroutine(ActivateShield());
        statistics.SetBlocks(0);
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
