using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevonCombat : MonoBehaviour, ICombat
{
    private Animator _animator;
    private float _timeStamp;
    
    public class AnimationSettings
    {
        //Use these names to change the parameters value's of the  animator, to change the animation to it's inteded state.
        public string punch = "Punch";
        public string kick = "Kick";
    }
    public AnimationSettings animations;

    public class CombatSettings
    {
        //Combat settings
        public float punchCooldown = 1;
        public float kickCooldown = 1;
    }
    public CombatSettings combat;

    public float TimeStamp
    {
        get { return _timeStamp; }
    }

    public Animator Animator
    {
        get { return _animator; }
        set { _animator = value; }
    }

    private void Start()
    {
        print("get animator");
       // _animator  = this.transform.GetComponent<CharacterController>().animator;
    }

    public void Attack1()
    {
        print("Preform attack1");
        //Destroy(this);
     //   _animator.SetTrigger("Punch");
        _timeStamp = Time.time + 1;
        //print(combat.punchCooldown);
        //_timeStamp = combat.punchCooldown;
    }

    public void Attack2()
    {
        //_animator.SetTrigger(animations.kick);
      //  _timeStamp = Time.time + combat.kickCooldown;
    }

    public void Attack3()
    {
        throw new System.NotImplementedException();
    }


}
