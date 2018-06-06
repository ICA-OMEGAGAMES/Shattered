using System.Collections;
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
            //("Possess Used");
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
