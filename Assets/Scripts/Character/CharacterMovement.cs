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
        public string groundedBool = "isGrounded";
        public string jumpBool = "isJumping";
        public string crouchBool = "isCrouching";
        public string dodgeBool = "isDodging";
        public string isInCombat = "isInCombat";
        public string verticalVelocityFloat = "Forward";
        public string horizontalVelocityFloat = "Strafe";
        public string attack1 = "Attack1";
        public string attack2 = "Attack2";
        public string weaponSet = "WeaponSet";
        public string deadBool = "isDead";
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
        public float jumpTime = 0.25f;
        public float jumpCooldown = 1;
        public float dodgeDistance = 10;
        public float toggleCombatCooldown = 1;
        public float rotateSpeed = 5;
        public float respawnTime = 5;
    }
	[SerializeField]
	public MovementSettings movement;

    //protected variables
    protected bool combatState = false;
    protected bool characterRooted = true;

    //public variables
    public float characterActionTimeStamp =0;
    public bool crouching;
    public bool dodging;
    public bool characterControllable = true;

    //private variables
    private bool jumping;
    private float speed;
    private float characterToggleCombatTimeStamp = 0;

    public Animator animator;
	private CharacterController characterController;
	private Vector3 moveDirection;
    private Statistics statistics;

    //characterscript spesific updates
    protected virtual void CharactertInitialize() { }
    protected virtual void CombatActionUpdate() { }
    protected virtual void CharacterInCombatUpdate() { }
    protected virtual void CharacterOutOfCombatUpdate() { }

    void Start()
    {
        statistics = this.transform.root.GetComponent<Statistics>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
        CharactertInitialize();
    }

    void Update()
    {
        if (characterControllable) {
            //actions only available durring
            if (statistics.GetHealth() == 0)
            {
                Die();
            }
            if (IsGrounded())
            {
                //instant actions
                if (Input.GetButton(Constants.CROUCH_BUTTON))
                    crouching = true;
                else
                    crouching = false;

                if (Input.GetButton(Constants.COMBAT_BUTTON) && characterToggleCombatTimeStamp <= Time.time)
                    SwitchCombatState();
                //Apply movementDirections
                moveDirection = new Vector3(Input.GetAxis(Constants.HORIZONTAL_AXIS), 0, Input.GetAxis(Constants.VERTICAL_AXIS));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= GetSpeed();

                //limit actions to the in/out combat state
                switch (combatState)
                {
                    case (false):
                        OutOfCombatUpdate();
                        break;
                    case (true):
                        InCombatUpdate();
                        break;
                }

                if (Input.GetButton(Constants.HORIZONTAL_AXIS) || Input.GetButton(Constants.VERTICAL_AXIS))
                {
                    RotateToCamera();
                }
            }
        }
        //movement
        if (characterRooted == false) {
            Animate(Input.GetAxis(Constants.VERTICAL_AXIS) * GetSpeed(), Input.GetAxis(Constants.HORIZONTAL_AXIS) * GetSpeed());
            moveDirection.y -= physics.gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    private void RotateToCamera()
    {
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y = Camera.main.transform.eulerAngles.y;

        Quaternion targetRotation = Quaternion.Euler(newRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * movement.rotateSpeed);
    }

    void OutOfCombatUpdate()
    {
        //call out of combat update of specific character
        CharacterOutOfCombatUpdate();
        //out of combat actions for both characters
        //cooldown based actions 
        if (characterActionTimeStamp <= Time.time)
        {
            characterRooted = false;
            if (Input.GetButton(Constants.JUMP_BUTTON) && crouching == false)
            {
                Jump();
                moveDirection.y = movement.jumpSpeed;
            }
        }
    }

    void InCombatUpdate()
    {
        //call in combat update of specific character
        CharacterInCombatUpdate();
        //incombat actions for both characters
        //cooldown based actions 
        if (characterActionTimeStamp <= Time.time)
        {
            characterRooted = false;
            if (combatState == true && crouching == false)
            {
                if (Input.GetButton(Constants.ATTACK1_BUTTON) || Input.GetButton(Constants.ATTACK2_BUTTON))
                {
                    CombatActionUpdate();
                }
            }
        }
    }

    //Animates the character and root motion handles the movement
    public void Animate(float forward, float strafe)
    {
        animator.SetFloat(animations.verticalVelocityFloat, forward);
        animator.SetFloat(animations.horizontalVelocityFloat, strafe);
        animator.SetBool(animations.jumpBool, jumping);
        animator.SetBool(animations.groundedBool, IsFalling());
        animator.SetBool(animations.crouchBool, crouching);
        animator.SetBool(animations.dodgeBool, dodging);
    }

    //returns if the player is falling or not (Has to be slightly bigger as IsGrounded()
    private bool IsFalling()
    {
        float distToGround = 0.2f;
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
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
    
    public void SwitchCombatState()
    {
        if (combatState)
            combatState = false;
        else
            combatState = true;

        animator.SetBool(animations.isInCombat, combatState);
        characterToggleCombatTimeStamp = Time.time + movement.toggleCombatCooldown;
    }

    private void Die()
    {
        //disable input
        characterControllable = false;
        //start animation death scene
        animator.SetBool(animations.deadBool,true);
        StartCoroutine(Respawn());
        ///find last checkpoint. after X respawn, set controllable true

    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(movement.respawnTime);
        this.transform.position = statistics.spawnpoint.transform.position;
    }
}