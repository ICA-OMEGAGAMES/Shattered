using System;
using UnityEngine;

[Serializable]
public class AIAnimationManager : MonoBehaviour
{
    public Animator animator { get; private set; }

    private bool isDodging;
    private bool isLooking;
    private float lookingTimestamp;
    private bool isBlocking;
    private float blockingTimestamp;
    private string lastAttack;

    [System.Serializable]
    public class AnimationSettings
    {
        //Use these names to change the parameters value's of the  animator, to change the animation to it's inteded state.
        public string groundedBool = "isGrounded";
        public string dodge = "Dodging";
        public string lookArounBool = "isLooking";
        public string isInCombat = "isInCombat";
        public string deadBool = "isDead";
        public string blockBool = "isBlocking";
        public string verticalVelocityFloat = "Forward";
        public string weaponSet = "WeaponSet";
        public string attack1 = "Attack1";
        public string attack2 = "Attack2";
        public string attacked = "Hit";
    }

    [SerializeField]
    public AnimationSettings animations;

    public void SetUpAIAnimationManager()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(isBlocking && blockingTimestamp < Time.time)
        {
            isBlocking = false;
        }
        if(isLooking && lookingTimestamp < Time.time)
        {
            isLooking = false;
        }
    }

    public void Animate(float walkingSpeed)
    {
        animator.SetFloat(animations.verticalVelocityFloat, walkingSpeed);
        animator.SetBool(animations.groundedBool, IsFalling());
        if(isDodging){
            animator.SetTrigger(animations.dodge);
            isDodging = false;
        }
        animator.SetBool(animations.lookArounBool, isLooking);
        animator.SetBool(animations.blockBool, isBlocking);
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

    public bool Dodge(bool dodge)
    {
        isDodging = dodge;
        return isDodging;
    }

    public bool Block(bool block, float duration)
    {
        isBlocking = block;
        if(isBlocking)
        {
            blockingTimestamp = Time.time + duration;
        }
        return isBlocking;
    }

    public void LookAround(float duration)
    {
        isLooking = true;
        lookingTimestamp = Time.time + duration;
    }

     public void StopLooking()
    {
        isLooking = false;
    }

    public bool IsBlocking()
    {
        return isBlocking;
    }

    public bool IsLooking()
    {
        return isLooking;
    }

    public void HitRegistered()
    {
        animator.SetTrigger(animations.attacked);
        animator.Play(Constants.ANIMATIONSTATE_HIT);
    }

    public string GetLastAttack()
    {
        return lastAttack;
    }

}