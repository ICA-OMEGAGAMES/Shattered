using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InventoryController))]
[RequireComponent(typeof(Yarn.Unity.Shattered.DialoguePlayer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterAudioController))]
public class DevonIndoorScript : MonoBehaviour
{

    //Serialized classes
    [System.Serializable]
    public class AnimationSettings
    {
        //Use these names to change the parameters value's of the  animator, to change the animation to it's inteded state.
        public string groundedBool = "isGrounded";
        public string verticalVelocityFloat = "Forward";
        public string horizontalVelocityFloat = "Strafe";
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
        public float rotateSpeed = 5;
    }
    [SerializeField]
    public MovementSettings movement;

    //protected variables
    protected bool characterRooted = false;
    public bool characterControllable = true;

    public Animator animator;
    public CharacterController characterController;
    private Vector3 moveDirection;
    private Statistics statistics;
    public CharacterAudioController characterAudio;

    void Start()
    {
        statistics = this.transform.root.GetComponentInChildren<Statistics>();
        characterAudio = this.transform.root.GetComponent<CharacterAudioController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
    }

    void Update()
    {
        if (characterControllable)
        {
            SetControllable(true);

            //Apply movementDirections
            moveDirection = new Vector3(Input.GetAxis(Constants.HORIZONTAL_AXIS), 0, Input.GetAxis(Constants.VERTICAL_AXIS));
            if (moveDirection != Vector3.zero)
            {
                characterAudio.InvokeWalkingSoundsCoroutine();
            }
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= movement.walkSpeed;
            
            if (Input.GetButton(Constants.HORIZONTAL_AXIS) || Input.GetButton(Constants.VERTICAL_AXIS))
            {
                RotateToCamera();
            }
            if (characterRooted == false)
            {
                AnimateMovement(Input.GetAxis(Constants.VERTICAL_AXIS) * movement.walkSpeed, Input.GetAxis(Constants.HORIZONTAL_AXIS) * movement.walkSpeed);
                moveDirection.y -= physics.gravity * Time.deltaTime;
                characterController.Move(moveDirection * Time.deltaTime);
            }
        }
        else
            SetControllable(false);
        Animate();
    }
        

    private void RotateToCamera()
    {
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y = Camera.main.transform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(newRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * movement.rotateSpeed);
    }

    //Animates the character and root motion handles the movement
    public void AnimateMovement(float forward, float strafe)
    {
        animator.SetFloat(animations.verticalVelocityFloat, forward);
        animator.SetFloat(animations.horizontalVelocityFloat, strafe);
    }

    public void Animate()
    {
        animator.SetBool(animations.groundedBool, IsFalling());
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

    public float pushPower = 2.0f;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
        ItemAudio itemAudio = hit.collider.gameObject.GetComponent(typeof(ItemAudio)) as ItemAudio;
        itemAudio.InvokePlayEffectCoroutine();
    }
}
