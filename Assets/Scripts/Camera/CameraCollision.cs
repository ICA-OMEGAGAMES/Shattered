
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
	private CameraCollisionSettings cameraCollisionSettings;
    private Vector3 dollyDir;

    // Use this for initialization
    void Start()
    {

        dollyDir = transform.localPosition.normalized;

        cameraCollisionSettings = ScriptableObject.CreateInstance<CameraCollisionSettings>(); 

		cameraCollisionSettings.distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * cameraCollisionSettings.maxDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
        {
			cameraCollisionSettings.distance = Mathf.Clamp((hit.distance * 0.87f), cameraCollisionSettings.minDistance, cameraCollisionSettings.maxDistance);
        }
        else
        {
			cameraCollisionSettings.distance = cameraCollisionSettings.maxDistance;
        }

		transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * cameraCollisionSettings.distance, Time.deltaTime * cameraCollisionSettings.smooth);

	}
}