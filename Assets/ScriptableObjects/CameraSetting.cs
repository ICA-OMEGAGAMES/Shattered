using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "CameraFollowData", menuName = "Shattered/Camera/FollowSettings", order = 1)]
public class CameraFollowSettings : ScriptableObject {
    public float cameraMoveSpeed = 120.0f;
    public float clampAngle = 66.0f;
    public float inputSensitivity = 150.0f;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
}

[CreateAssetMenu(fileName = "CameraCollisionData", menuName = "Shattered/Camera/CollisionSettings", order = 2)]
public class CameraCollisionSettings : ScriptableObject {
    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    public float smooth = 10.0f;
    public float distance;
}
