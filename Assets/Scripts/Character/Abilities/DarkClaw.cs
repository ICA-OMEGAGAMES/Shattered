using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkClaw : ISkill
{
    private SkillSettings settings;
    private MonoBehaviour mono;

    private float cooldownTimestamp;

    public DarkClaw(SkillSettings settings, MonoBehaviour mono)
    {
        this.settings = settings;
        this.mono = mono;
    }

    public void Execute(Animator animator)
    {
        if (!IsOnCooldown())
        {
            cooldownTimestamp = Time.time + settings.cooldown;
            //("DarkClaw Used");
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