﻿//-------------------------------------------------------------------------
// Author:			Benjamin Grabherr                    Date: 17.04.2018
// Description:		This is a camera Collision Script in
// 					a third person view. If the player-camera
//					hit an object, the cam will automaticly 
//					go near the player.
// Source:			https://www.youtube.com/watch?v=LbDQHv9z-F0&t=127s
//
//-------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
	[SerializeField] CameraCollisionSetting cameraCollisionSetting;
    private Vector3 dollyDir;

    // Use this for initialization
    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
		cameraCollisionSetting.distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * cameraCollisionSetting.maxDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
        {
			cameraCollisionSetting.distance = Mathf.Clamp((hit.distance * 0.87f), cameraCollisionSetting.minDistance, cameraCollisionSetting.maxDistance);
        }
        else
        {
			cameraCollisionSetting.distance = cameraCollisionSetting.maxDistance;
        }

		transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * cameraCollisionSetting.distance, Time.deltaTime * cameraCollisionSetting.smooth);
    }
}
