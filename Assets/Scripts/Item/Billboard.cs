using UnityEngine;


public class Billboard : MonoBehaviour
{
	public Camera main;

	void Update()
	{
		transform.LookAt (transform.position + main.transform.rotation * Vector3.forward, main.transform.rotation * Vector3.up);
	}
}