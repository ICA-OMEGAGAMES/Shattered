using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(UnityEngine.CharacterController))]
public class CharacterScript : MonoBehaviour
{
    //Serialized classes
    [System.Serializable]
    public class AnimationSettings
    {
        //Use these names to change the parameters value's of the  animator, to change the animation to it's inteded state.
        public string verticalVelocityFloat = "Forward";
        public string horizontalVelocityFloat = "Strafe";
        public string groundedBool = "isGrounded";
        public string jumpBool = "isJumping";
        public string crouchBool = "isCrouching";
        public string dodgeBool = "isDodging";
        public string punch = "Punch";
        public string kick = "Kick";
        public string isInCombat = "isInCombat";
    }
    [SerializeField]
    public AnimationSettings animations;

    [System.Serializable]
    public class PhysicsSettings
    {
        public float gravity = 20.0F;
        public LayerMask groundLayers;
    }
    [SerializeField]
    public PhysicsSettings physics;

    [System.Serializable]
    public class MovementSettings
    {
        public float walkSpeed = 4.0F;
        public float crouchSpeed = 2.0F;
        public float runSpeed = 8.0F;
        public float jumpSpeed = 8.0F;
        public float jumpTime = 0.5f;
        public float jumpCooldown = 1;
        public float dodgeDistance = 10;
    }
    [SerializeField]
    public MovementSettings movement;

    [System.Serializable]
    public class UnarmedCombat
    {
        public float punchCooldown = 1;
        public float kickCooldown = 0.5f;
        public float toggleCombatCooldown = 1;
    }
    [SerializeField]
    public UnarmedCombat combat;

    //public variables
    public float characterActionTimeStamp;

    //private variables
    private bool jumping;
    private bool dodging;
    private bool crouching;
    private float speed;
    private bool combatState = false;
    public bool characterRooted = true;

    public Animator animator;
	private CharacterController characterController;
	private Vector3 moveDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
    }

    void Update()
    {
        //actions only available durring
        if (IsGrounded())
        {
            //instant actions
			if (Input.GetButton(Constants.CROUCH_BUTTON))
                crouching = true;
            else
                crouching = false;

			if (Input.GetButton(Constants.DODGE_BUTTON))
                Dodge();

            //set speed
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= GetSpeed();

            //cooldown based actions 
            if (characterActionTimeStamp <= Time.time)
            {
                characterRooted = false;
                if (Input.GetButton(Constants.JUMP_BUTTON))
                {
                    Jump();
                    moveDirection.y = movement.jumpSpeed;
                }
                if (Input.GetButton(Constants.COMBAT_BUTTON))
                {
                    SwitchCombatState();
                }
                if (Input.GetButton(Constants.ATTACK1_BUTTON))
                {
                    print("gopunch");
                    punch();
                }
                else if (Input.GetButton(Constants.ATTACK2_BUTTON))
                {
                    kick();
                }
            }
        }
        //movement
        if (characterRooted == false) { 
            Animate(Input.GetAxis("Vertical") * GetSpeed(), Input.GetAxis("Horizontal")* GetSpeed());
            moveDirection.y -= physics.gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    //Animates the character and root motion handles the movement
    public void Animate(float forward, float strafe)
    {
        animator.SetFloat(animations.verticalVelocityFloat, forward);
        animator.SetFloat(animations.horizontalVelocityFloat, strafe);
        animator.SetBool(animations.jumpBool, jumping);
        animator.SetBool(animations.groundedBool, IsGrounded());
        animator.SetBool(animations.crouchBool, crouching);
        animator.SetBool(animations.dodgeBool, dodging);
    }

	private bool IsGrounded()
	{
		float distToGround = 0.1f;
		return Physics.Raycast(transform.position, -Vector3.up, distToGround);
	}

    private float GetSpeed()
    {
		if (Input.GetButton(Constants.RUN_BUTTON))
            speed = movement.runSpeed;
		else if (Input.GetButton(Constants.CROUCH_BUTTON))
            speed = movement.crouchSpeed;
        else
            speed = movement.walkSpeed;
        return speed;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            jumping = true;
            characterActionTimeStamp = Time.time + movement.jumpCooldown;
            StartCoroutine(StopJump());
        }
    }

    //Stops from jumping
    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(movement.jumpTime);
        jumping = false;
    }

    private void Dodge()
    {
        if (!dodging)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                StartCoroutine(Roll(true, Input.GetAxis("Horizontal")));
            }
            else if(Input.GetButton("Vertical"))
            {
                StartCoroutine(Roll(false, Input.GetAxis("Vertical")));
            }
        }
    }

    IEnumerator Roll(bool horizontal, float direction)
    {
        dodging = true;
        yield return new WaitForSeconds(0.5f);
        StopCoroutine(Roll(horizontal, direction));
        dodging = false;
    }


    private void punch()
    {
        characterRooted = true;
        print("punch");
        if (combatState == false)
            SwitchCombatState();
        characterActionTimeStamp = Time.time + combat.punchCooldown;
        animator.SetTrigger(animations.punch);
    }

    private void kick()
    {
        characterRooted = true;
        print("kick");
        if (combatState == false)
            SwitchCombatState();
        characterActionTimeStamp = Time.time + combat.kickCooldown;
        animator.SetTrigger(animations.kick);
    }

    public void SwitchCombatState()
    {
        if (combatState)
            combatState = false;
        else
            combatState = true;

        animator.SetBool(animations.isInCombat, combatState);
        characterActionTimeStamp = Time.time + combat.toggleCombatCooldown;
    }
}