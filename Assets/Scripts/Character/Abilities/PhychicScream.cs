using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsychicScream : ISkill
{
    private SkillSettings settings;
    private MonoBehaviour mono;
    private MalphasScript.SkillAnimations skillAnimations = new MalphasScript.SkillAnimations();

    private float cooldownTimestamp;

    public PsychicScream(SkillSettings settings, MonoBehaviour mono)
    {
        this.settings = settings;
        this.mono = mono;
    }

    public void Execute(Animator animator)
    {
        if (!IsOnCooldown())
        {
            cooldownTimestamp = Time.time + settings.cooldown;
            //("PsychicScream Used");
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
