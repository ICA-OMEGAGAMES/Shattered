using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;
    
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
        public float dodgeDistance = 10;
    }
    [SerializeField]
    public MovementSettings movement;

    //private variables
    private bool jumping;
    private bool dodging;
    private bool crouching;
    private float speed;
    private Vector3 moveDirection = Vector3.zero;
    
    private bool IsGrounded()
    {
        float distToGround = 0.1f;
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }

    void Start()
    {
        animator = this.transform.GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            if (Input.GetButton("Crouch"))
            {
                crouching = true;
            }
            else
            {
                crouching = false;
            }

            if (Input.GetButton("Dodge"))
            {
                dodge();
            }

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
  
            speed = GetSpeed();
            moveDirection *= speed;

            //jump
            if (Input.GetButton("Jump"))
            {
                Jump();
                moveDirection.y = movement.jumpSpeed;
            }
        }

        //apply
        Animate(Input.GetAxis("Vertical") * GetSpeed(), Input.GetAxis("Horizontal")* GetSpeed());
        moveDirection.y -= physics.gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
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

    private float GetSpeed()
    {
        if (Input.GetButton("Run"))
            speed = movement.runSpeed;
        else if (Input.GetButton("Crouch"))
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

    private void dodge()
    {
        if (!dodging)
        {
            if (Input.GetAxis("Horizontal") !=0)
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
}
