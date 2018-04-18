using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();	
	}
	
	// Update is called once per frame
	void Update () {

		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		rb.AddForce(new Vector3(horizontal, 0.0f, vertical) * 10);
	}


	void OnTriggerStay (Collider other)
	{
		if(Input.GetButtonDown("Pickup"))
			if (other.gameObject.CompareTag ("Item"))
				other.gameObject.SetActive (false);
	}
}
