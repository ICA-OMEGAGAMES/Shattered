using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{

	[SerializeField]
	public AnimationSettings animations;
	[SerializeField]
	public PhysicsSettings physics;
	[SerializeField]
	public MovementSettings movement;

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
    }

	[System.Serializable]
	public class PhysicsSettings
	{
		public float gravity = 20.0F;
		public LayerMask groundLayers;
	}
		
    [System.Serializable]
    public class MovementSettings
    {
        public float walkSpeed = 4.0F;
        public float crouchSpeed = 2.0F;
        public float runSpeed = 8.0F;
        public float jumpSpeed = 8.0F;
        public float jumpTime = 0.25f;
        public float dodgeDistance = 10;
    }

    //private variables
    private bool jumping;
    private bool dodging;
    private bool crouching;
    private float speed;
	private Animator animator;
	private CharacterController characterController;
	private Vector3 moveDirection;

    void Start()
    {
        animator = this.transform.GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
		moveDirection = Vector3.zero;
    }


    void Update()
    {
        if (IsGrounded())
        {
			if (Input.GetButton(Constants.CROUCH_BUTTON))
                crouching = true;
            else
                crouching = false;

			if (Input.GetButton(Constants.DODGE_BUTTON))
                Dodge();

			moveDirection = new Vector3(Input.GetAxis(Constants.HORIZONTAL_AXIS), 0, Input.GetAxis(Constants.VERTICAL_AXIS));
            moveDirection = transform.TransformDirection(moveDirection);
  
            speed = GetSpeed();
            moveDirection *= speed;

			if (Input.GetButton(Constants.JUMP_BUTTON))
            {
                Jump();
                moveDirection.y = movement.jumpSpeed;
            }
        }
			
		Animate(Input.GetAxis(Constants.VERTICAL_AXIS) * GetSpeed(), Input.GetAxis(Constants.HORIZONTAL_AXIS) * GetSpeed());
        moveDirection.y -= physics.gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

	void FixedUpdate(){
		RotateCharacterWithCamera ();
	}

	private void RotateCharacterWithCamera(){
		Vector3 newRotation = transform.eulerAngles;
		newRotation.y = Camera.main.transform.eulerAngles.y;
		transform.eulerAngles = newRotation;
	}

    //Animates the character and root motion handles the movement
    public void Animate(float forward, float strafe)
    {
        animator.SetFloat(animations.verticalVelocityFloat, forward);
        animator.SetFloat(animations.horizontalVelocityFloat, strafe);
        animator.SetBool(animations.groundedBool, IsGrounded());
        animator.SetBool(animations.jumpBool, jumping);
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
			if (Input.GetAxis(Constants.HORIZONTAL_AXIS) != 0)
            {
				StartCoroutine(Roll(true, Input.GetAxis(Constants.HORIZONTAL_AXIS)));
            }
			else if(Input.GetAxis(Constants.VERTICAL_AXIS) != 0)
            {
				StartCoroutine(Roll(false, Input.GetAxis(Constants.VERTICAL_AXIS)));
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
}
