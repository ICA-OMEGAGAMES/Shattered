
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
	[System.Serializable]
	public class CameraCollisionSettings
	{
		public float minDistance = 1.0f;
		public float maxDistance = 4.0f;
		public float smooth = 10.0f;
		public float distance;
	}
	[SerializeField]
	public CameraCollisionSettings cameraCollisionSetting;

    private Vector3 dollyDir;

    // Use this for initialization
    void Start()
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
