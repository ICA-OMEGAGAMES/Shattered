using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

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
        animator.SetFloat("Speed", move);

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                animator.SetTrigger(jumpHash);
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
