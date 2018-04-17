using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{

    Animator animator;
    CharacterController characterController;
    
    [System.Serializable]
    public class AnimationSettings
    {
        public string verticalVelocityFloat = "Forward";
        public string horizontalVelocityFloat = "Strafe";
        public string groundedBool = "IsGrounded";
        public string jumpBool = "IsJumping";
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
        public float runSpeed = 8.0F;
        public float jumpSpeed = 8.0F;
        public float jumpTime = 0.25f;
    }
    [SerializeField]
    public MovementSettings movement;

    bool jumping;
    private float speed;

    private Vector3 moveDirection = Vector3.zero;


    bool isGrounded()
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
        
        if (isGrounded())
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //walk/run

            speed = getSpeed();
            

            moveDirection *= speed;
            //animator.SetFloat("Speed", move *= speed);
            //jump
            if (Input.GetButton("Jump"))
            {
                Jump();
                moveDirection.y = movement.jumpSpeed;
            }
        }
        else{
            
            characterController.Move(Vector3.down * 2 * Time.deltaTime);
            print("notGrounded");
        }

        Animate(Input.GetAxis("Vertical") * getSpeed(), Input.GetAxis("Horizontal")* getSpeed());
        //apply
        moveDirection.y -= physics.gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
    //Animates the character and root motion handles the movement
    public void Animate(float forward, float strafe)
    {
        animator.SetFloat(animations.verticalVelocityFloat, forward);
        animator.SetFloat(animations.horizontalVelocityFloat, strafe);
      //  animator.SetBool(animations.groundedBool, isGrounded());
        animator.SetBool(animations.jumpBool, jumping);
    }

    public float getSpeed()
    {
        if (Input.GetButton("Run"))
            speed = movement.runSpeed;
        else
            speed = movement.walkSpeed;
        return speed;
    }

    public void Jump()
    {
        if (isGrounded())
        {
            jumping = true;
            StartCoroutine(StopJump());
        }
    }

    //Stops us from jumping
    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(movement.jumpTime);
        jumping = false;
    }
}
