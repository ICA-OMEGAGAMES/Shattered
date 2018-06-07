using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "CameraFollowData", menuName = "CameraSettings/Follow", order = 1)]
public class CameraFollowSettings : ScriptableObject {
    public float cameraMoveSpeed = 1200.0f;
    public float clampAngle = 66.0f;
    public float inputSensitivity = 150.0f;
    public float finalInputX = 0;
	public float finalInputY = 0;
}

[CreateAssetMenu(fileName = "CameraCollisionData", menuName = "CameraSettings/Collision", order = 2)]
public class CameraCollisionSettings : ScriptableObject {
    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    public float smooth = 10.0f;
    public float distance = 0;
}