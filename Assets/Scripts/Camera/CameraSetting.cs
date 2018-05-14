//-------------------------------------------------------------------------
// Author:			Benjamin Grabherr                    Date: 17.04.2018
// Description:		This is a Scriptable Object
//                  for defining the camera follow &
//                  collision settings.
// Source:			
//
//-------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "CameraFollowData", menuName = "CameraSetting/Follow", order = 1)]
public class CameraFollowSetting : ScriptableObject {
    public float cameraMoveSpeed = 120.0f;
    public float clampAngle = 66.0f;
    public float inputSensitivity = 150.0f;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
}

[CreateAssetMenu(fileName = "CameraCollisionData", menuName = "CameraSetting/Collision", order = 2)]
public class CameraCollisionSetting : ScriptableObject {
    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    public float smooth = 10.0f;
    public float distance;
}
