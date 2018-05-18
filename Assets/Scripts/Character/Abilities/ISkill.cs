using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    void Execute(Animator animator);
    bool IsOnCooldown();
}
