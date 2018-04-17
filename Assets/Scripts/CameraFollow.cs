//-------------------------------------------------------------------------
// Author:			Benjamin Grabherr                    Date: 16.04.2018
// Description:		This is a camera Follow Script in
// 					a third person view.
// Source:			https://www.youtube.com/watch?v=LbDQHv9z-F0&t=127s
//
//-------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] CameraFollowSetting myFollowData;
    [SerializeField] GameObject cameraFollowObj;
    // [SerializeField] GameObject cameraObj;
    // [SerializeField] GameObject playerObj;
    private Vector3 followPOS;
	private float rotY = 0.0f;
    private float rotX = 0.0f;

    // public float smoothX;
    // public float smoothY;
	// public float camDistanceXToPlayer;
    // public float camDistanceYToPlayer;
    // public float camDistanceZToPlayer;


    // Use this for initialization
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        // Block the cursor & make him unvisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Setup the rotation of the sticks here --> Supports also the Controller
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");
        myFollowData.mouseX = Input.GetAxis("Mouse X");
        myFollowData.mouseY = Input.GetAxis("Mouse Y");
        myFollowData.finalInputX = inputX + myFollowData.mouseX;
        myFollowData.finalInputZ = inputZ + myFollowData.mouseY;

        // Rotate the stick, depending where we pushing
        rotY += myFollowData.finalInputX * myFollowData.inputSensitivity * Time.deltaTime;
        rotX += myFollowData.finalInputZ * myFollowData.inputSensitivity * Time.deltaTime;

        // Clamp that value, so it can't go higher or lower --> stop it from going around and around in circles
        rotX = Mathf.Clamp(rotX, -myFollowData.clampAngle, myFollowData.clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    void LateUpdate()
    {
        CameraUpdater();
    }

    private void CameraUpdater()
    {
        // set the target object to follow
        Transform target = cameraFollowObj.transform;

        // move towards the game object that is the target
        float step = myFollowData.cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
