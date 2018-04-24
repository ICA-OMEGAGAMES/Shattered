using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
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
        public float toggleCombatCooldown = 1;
    }
    [SerializeField]
    public MovementSettings movement;

    //public variables
    public float characterActionTimeStamp;

    //private variables
    private bool jumping;
    private bool dodging;
    private bool crouching;
    private float speed;
    protected bool combatState = false;
    protected bool characterRooted = true;

    public Animator animator;
	private CharacterController characterController;
	private Vector3 moveDirection;

    protected virtual void CombatInitialize() { }
    protected virtual void CombatActionUpdate() { }
    protected virtual void CombatSetUpdate() { }

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
        CombatInitialize();
    }

    void Update()
    {
        //todo: IF(pickupItem) setCombatStance(itemSorth);
        CombatSetUpdate();

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
                if (Input.GetButton(Constants.ATTACK1_BUTTON) || Input.GetButton(Constants.ATTACK2_BUTTON))
                {
                    CombatActionUpdate();
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

    //returns if the player is grounded or not
	private bool IsGrounded()
	{
		float distToGround = 0.1f;
		return Physics.Raycast(transform.position, -Vector3.up, distToGround);
	}

    //select correct movement speed
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
    
    public void SwitchCombatState()
    {
        if (combatState)
            combatState = false;
        else
            combatState = true;

        animator.SetBool(animations.isInCombat, combatState);
        characterActionTimeStamp = Time.time + movement.toggleCombatCooldown;
    }
}