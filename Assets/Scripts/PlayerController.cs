using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float walkSpeed = 4.0F;
    public float runSpeed = 8.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    private float speed;
    Animator animator;
    int jumpHash = Animator.StringToHash("Jump");
    // Use this for initialization
    void Start () {
        animator = this.transform.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        CharacterController controller = GetComponent<CharacterController>();

        float move = Input.GetAxis("Vertical");
        

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //walk/run
            if (Input.GetButton("Run"))
                speed = runSpeed;
            else
                speed = walkSpeed;

            moveDirection *= speed;
            animator.SetFloat("Speed", move*=speed);
            //jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                animator.SetTrigger(jumpHash);
            }
        }

        //apply
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
