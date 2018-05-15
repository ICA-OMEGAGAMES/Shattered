using UnityEngine;
using Yarn.Unity.Shattered;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] CameraFollowSetting cameraFollowSetting;
    [SerializeField] GameObject gameObjectToFollow;
    [SerializeField] DialogueRunner dialogueRunner;

	private float rotY = 0.0f;
    private float rotX = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        // Block the cursor & make him unvisible
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckActiveCamera();
        if (dialogueRunner.isDialogueRunning) {
            // Unlock the cursor if dialogue is running.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            // Lock Cursor, if Camera is active
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Setup the rotation of the sticks here --> Supports also the Controller
            float inputX = Input.GetAxis("RightStickHorizontal");
            float inputZ = Input.GetAxis("RightStickVertical");
            cameraFollowSetting.mouseX = Input.GetAxis(Constants.MOUSE_X_AXIS);
            cameraFollowSetting.mouseY = Input.GetAxis(Constants.MOUSE_Y_AXIS);
            cameraFollowSetting.finalInputX = inputX + cameraFollowSetting.mouseX;
            cameraFollowSetting.finalInputZ = inputZ + cameraFollowSetting.mouseY;

            // Rotate the stick, depending where we pushing
            rotY += cameraFollowSetting.finalInputX * cameraFollowSetting.inputSensitivity * Time.deltaTime;
            rotX += cameraFollowSetting.finalInputZ * cameraFollowSetting.inputSensitivity * Time.deltaTime;

            // Clamp that value, so it can't go higher or lower --> stop it from going around and around in circles
            rotX = Mathf.Clamp(rotX, -cameraFollowSetting.clampAngle, cameraFollowSetting.clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        }
    }

    private void CheckActiveCamera()
    {
        if (!gameObjectToFollow.activeInHierarchy)
        {
            gameObjectToFollow = GameObject.Find("CameraFollowPoint");
        }
    }

    void LateUpdate()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        // set the target object to follow
		Transform target = gameObjectToFollow.transform;

        // move towards the game object that is the target
		float step = cameraFollowSetting.cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
