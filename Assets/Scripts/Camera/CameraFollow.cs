using UnityEngine;
using Yarn.Unity.Shattered;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    public GameObject gameObjectToFollow;

    private CameraFollowSettings cameraFollowSettings;

    [SerializeField] DialogueRunner dialogueRunner;

    private float rotY = 0.0f;
    private float rotX = 0.0f;

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        cameraFollowSettings = ScriptableObject.CreateInstance<CameraFollowSettings>();

        // Block the cursor & make him unvisible
        Cursor.visible = false;
    }

    void Update()
    {
        CheckActiveCamera();
        if (dialogueRunner.isDialogueRunning)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (SkillTreeMenu.skillTreeMenuIsActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Lock Cursor, if Camera is active
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            doCameraRotation();
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

    private void doCameraRotation()
    {
        cameraFollowSettings.finalInputX = Input.GetAxis(Constants.MOUSE_X_AXIS);
        cameraFollowSettings.finalInputZ = Input.GetAxis(Constants.MOUSE_Y_AXIS);

        // Rotate the stick, depending where we pushing
        //rotY += cameraFollowSettings.finalInputX * cameraFollowSettings.inputSensitivity * Time.deltaTime;
        //rotX += cameraFollowSettings.finalInputZ * cameraFollowSettings.inputSensitivity * Time.deltaTime;
        rotY += Input.GetAxis(Constants.MOUSE_X_AXIS) * cameraFollowSettings.inputSensitivity * Time.deltaTime;
        rotX += Input.GetAxis(Constants.MOUSE_Y_AXIS) * cameraFollowSettings.inputSensitivity * Time.deltaTime;
        // Clamp that value, so it can't go higher or lower --> stop it from going around and around in circles
        rotX = Mathf.Clamp(rotX, -cameraFollowSettings.clampAngle, cameraFollowSettings.clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }
}
