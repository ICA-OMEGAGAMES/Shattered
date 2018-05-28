using System;
using UnityEngine;

[Serializable]
public class AIAnimationManager : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    private bool dodging;

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
    
    public void Animate(float walkingSpeed)
    {
        animator.SetFloat(animations.verticalVelocityFloat, walkingSpeed);
        animator.SetBool(animations.groundedBool, IsFalling());
        animator.SetBool(animations.dodgeBool, dodging);
        dodging = false;
    }

    public void SetFightingAnimation(int fightMode, int attack)
    {
        animator.SetInteger(animations.weaponSet, fightMode);
        if(attack == 1)
        {
            animator.SetTrigger(animations.attack1);
        }
        else if (attack == 2)
        {
            animator.SetTrigger(animations.attack2);
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
        dodging = dodge;
        return dodging;
    }

}