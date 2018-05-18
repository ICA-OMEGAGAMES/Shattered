using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicWave : MonoBehaviour,ISkill{
    SkillSettings settings;

    private float cooldownTimestamp;

    public DemonicWave(SkillSettings settings)
    {
        this.settings = settings;
    }

    public void Execute(Animator animator)
    {
        if (!IsOnCooldown())
        {
            cooldownTimestamp = Time.time + settings.cooldown;
            print("DemonicWave Used");
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
