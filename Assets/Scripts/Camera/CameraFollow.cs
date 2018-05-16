using UnityEngine;
using Yarn.Unity.Shattered;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] 
	public GameObject gameObjectToFollow;

	[System.Serializable]
	public class CameraFollowSettings
	{
		public float cameraMoveSpeed = 120.0f;
		public float clampAngle = 66.0f;
		public float inputSensitivity = 150.0f;
		public float mouseX;
		public float mouseY;
		public float finalInputX;
		public float finalInputZ;
	}
	[SerializeField]
	public CameraFollowSettings cameraFollowSettings;

	[SerializeField] DialogueRunner dialogueRunner;

	private float rotY = 0.0f;
    private float rotX = 0.0f;
    
    void Start()
    {

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        // Block the cursor & make him unvisible
        Cursor.visible = false;
    }
    
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
            float inputX = Input.GetAxis(Constants.RIGHTSTICKHORIZONTAL);
            float inputZ = Input.GetAxis(Constants.RIGHTSTICKVERTICAL);
            cameraFollowSettings.mouseX = Input.GetAxis(Constants.MOUSE_X_AXIS);
            cameraFollowSettings.mouseY = Input.GetAxis(Constants.MOUSE_Y_AXIS);
            cameraFollowSettings.finalInputX = inputX + cameraFollowSettings.mouseX;
            cameraFollowSettings.finalInputZ = inputZ + cameraFollowSettings.mouseY;

            // Rotate the stick, depending where we pushing
            rotY += cameraFollowSettings.finalInputX * cameraFollowSettings.inputSensitivity * Time.deltaTime;
            rotX += cameraFollowSettings.finalInputZ * cameraFollowSettings.inputSensitivity * Time.deltaTime;

            // Clamp that value, so it can't go higher or lower --> stop it from going around and around in circles
            rotX = Mathf.Clamp(rotX, -cameraFollowSettings.clampAngle, cameraFollowSettings.clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        }
    }

    private void CheckActiveCamera()
    {
        if (!gameObjectToFollow.activeInHierarchy)
        {
            gameObjectToFollow = GameObject.Find(Constants.CAMERAFOLLOWPOINT);
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
		float step = cameraFollowSettings.cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
