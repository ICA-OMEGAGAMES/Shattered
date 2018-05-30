using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
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
        public string deadBool = "isDead";
        public string verticalVelocityFloat = "Forward";
        public string horizontalVelocityFloat = "Strafe";
        public string weaponSet = "WeaponSet";
        public string attack1 = "Attack1";
        public string attack2 = "Attack2";
        public string blink = "Blink";
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
        public float jumpCooldown = 0.5f;
        public float dodgeDistance = 10;
        public float toggleCombatCooldown = 1;
        public float rotateSpeed = 5;
    }
	[SerializeField]
	public MovementSettings movement;

    [System.Serializable]
    public class DeathSettings
    {
        public float respawnTime = 5;
        public float respawnHealth = 50;
    }
    [SerializeField]
    public DeathSettings death;

    //protected variables
    protected bool characterRooted = true;
    protected float characterActionTimeStamp = 0;
    protected bool crouching;
    protected bool dodging;

    //public variables
    public bool combatState = false;
    public bool characterControllable = true;

    //private variables
    private bool jumping;
    private float speed;
    private float characterToggleCombatTimeStamp = 0;

    public Animator animator;
	public CharacterController characterController;
	private Vector3 moveDirection;
    private Statistics statistics;

    //characterscript spesific updates
    protected virtual void CharactertInitialize() { }
    protected virtual void CombatActionUpdate() { }
    protected virtual void CharacterInCombatUpdate() { }
    protected virtual void CharacterOutOfCombatUpdate() { }
    protected virtual void CharacterInCombatFixedUpdate() { }
    protected virtual void CharacterOutOfCombatFixedUpdate() { }

    void Start()
    {
        statistics = this.transform.root.GetComponentInChildren<Statistics>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
        CharactertInitialize();
    }

    void Update()
    {
        if (characterControllable)
        {
            SetControllable(true);
            //actions only available durring
            if (statistics.GetHealth() == 0)
            {
                Die();
            }

            if (Input.GetButton(Constants.COMBAT_BUTTON) && characterToggleCombatTimeStamp <= Time.time)
                SwitchCombatState();

            if (IsGrounded())
            {
                //instant actions
                if (Input.GetButton(Constants.CROUCH_BUTTON))
                    crouching = true;
                else
                    crouching = false;

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
            else
                characterController.Move(transform.TransformDirection(new Vector3(0,0,0.01f)));

        }
        else
            SetControllable(false);

        //movement
        if (characterRooted == false) {
            Animate(Input.GetAxis(Constants.VERTICAL_AXIS) * GetSpeed(), Input.GetAxis(Constants.HORIZONTAL_AXIS) * GetSpeed());
            moveDirection.y -= physics.gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        switch (combatState)
        {
            case (false):
                CharacterOutOfCombatFixedUpdate();
                break;
            case (true):
                CharacterInCombatFixedUpdate();
                break;
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
            SetControllable(true);
            if (combatState == true && crouching == false)
            {
                if (Input.GetButton(Constants.ATTACK1_BUTTON) || Input.GetButton(Constants.ATTACK2_BUTTON))
                {
                    CombatActionUpdate();
                    SetControllable(false);
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
        animator.SetBool(animations.isInCombat, combatState);
    }

    private void SetControllable(bool active)
    {
        if (active)
            this.GetComponent<CharacterController>().enabled = true;
        else
            this.GetComponent<CharacterController>().enabled = false;
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
        characterToggleCombatTimeStamp = Time.time + movement.toggleCombatCooldown;
    }

    private void Die()
    {
        //disable input
        characterControllable = false;
        //start animation death scene
        animator.SetBool(animations.deadBool,true);
        //force death animation
        animator.Play(Constants.ANIMATIONSTATE_DEAD);
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(death.respawnTime);
        this.transform.position = statistics.Spawnpoint.transform.position;
        statistics.IncreaseHealth(death.respawnHealth);
        animator.SetBool(animations.deadBool, false);
        characterControllable = true;
    }

    public bool CombatState
    {
        get
        {
            return this.combatState;
        }
        set
        {
            this.combatState = value;
        }
    }
}