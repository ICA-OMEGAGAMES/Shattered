using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicWave : ISkill{
    SkillSettings settings;
    MonoBehaviour mono;

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
            //("DemonicWave Used");
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
