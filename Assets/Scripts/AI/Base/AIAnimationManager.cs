using System;
using UnityEngine;

[Serializable]
public class AIAnimationManager : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    private bool dodging;
    private float dodgingTimestamp;
    private bool blocking;
    private float blockingTimestamp;
    private string lastAttack;

    [System.Serializable]
    public class AnimationSettings
    {
        //Use these names to change the parameters value's of the  animator, to change the animation to it's inteded state.
        public string groundedBool = "isGrounded";
        public string dodgeBool = "isDodging";
        public string isInCombat = "isInCombat";
        public string deadBool = "isDead";
        public string verticalVelocityFloat = "Forward";
        public string weaponSet = "WeaponSet";
        public string attack1 = "Attack1";
        public string attack2 = "Attack2";
    }

    [SerializeField]
    public AnimationSettings animations;

    public void SetUpAIAnimationManager()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(blocking && blockingTimestamp < Time.time)
        {
            blocking = false;
        }
        if(dodging && dodgingTimestamp < Time.time)
        {
            dodging = false;
        }
    }

    public void Animate(float walkingSpeed)
    {
        animator.SetFloat(animations.verticalVelocityFloat, walkingSpeed);
        animator.SetBool(animations.groundedBool, IsFalling());
        animator.SetBool(animations.dodgeBool, dodging);
    }

    public void SetFightingAnimation(int fightMode, string attack)
    {
        animator.SetInteger(animations.weaponSet, fightMode);
        if (attack == Constants.ATTACK1_BUTTON)
        {
            animator.SetTrigger(animations.attack1);
            lastAttack = Constants.PUNCH_ATTACK;
        }
        else if (attack == Constants.ATTACK1_BUTTON)
        {
            animator.SetTrigger(animations.attack2);
            lastAttack = Constants.KICK_ATTACK;
        }

    }

    public void Die()
    {
        animator.SetBool(animations.deadBool, true);
        //force death animation
        animator.Play(Constants.ANIMATIONSTATE_DEAD);
    }

    private bool IsFalling()
    {
        float distToGround = 0.2f;
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }

    public bool Dodge(bool dodge, float duration)
    {
        dodging = dodge;
        if(dodging)
        {
            dodgingTimestamp = Time.time + duration;
        }
        return dodging;
    }

    public bool Block(bool block, float duration)
    {
        blocking = block;
        if(blocking)
        {
            blockingTimestamp = Time.time + duration;
        }
        return blocking;
    }

    public bool IsBlocking()
    {
        return blocking;
    }

    public string GetLastAttack()
    {
        return lastAttack;
    }

}