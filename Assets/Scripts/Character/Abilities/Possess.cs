using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possess : MonoBehaviour, ISkill
{
    SkillSettings settings;
    private float cooldownTimestamp;

    public Possess(SkillSettings settings)
    {
        this.settings = settings;
    }

    public void Execute(Animator animator)
    {
        if (!IsOnCooldown())
        {
            cooldownTimestamp = Time.time + settings.cooldown;
            print("Possess Used");
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
