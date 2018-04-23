using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat{

    void Attack1();
    void Attack2();
    void Attack3();
    float TimeStamp
    {
        get;
    }
    Animator Animator
    {
        get; set;
    }

}
