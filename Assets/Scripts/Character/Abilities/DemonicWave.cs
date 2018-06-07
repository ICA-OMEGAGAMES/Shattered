using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicWave : ISkill{
    private SkillSettings settings;
    private MonoBehaviour mono;
    private MalphasScript.SkillAnimations skillAnimations = new MalphasScript.SkillAnimations();
    

    private float cooldownTimestamp;

    public DemonicWave(SkillSettings settings, MonoBehaviour mono)
    {
        this.settings = settings;
        this.mono = mono;
    }

    public void Execute(Animator animator)
    {
        if (!IsOnCooldown())
        {
            cooldownTimestamp = Time.time + settings.cooldown;
            
            animator.SetTrigger(skillAnimations.demonicWave);
#pragma warning disable 0219
            GameObject aoeObject = GameObject.Instantiate(settings.skillEffect, mono.transform.position, mono.transform.rotation) as GameObject;
#pragma warning restore 0219
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
