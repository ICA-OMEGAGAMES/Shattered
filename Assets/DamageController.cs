using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player")) {
			other.GetComponent<Statistics> ().ReduceHealth (5);
		}
	}
}
