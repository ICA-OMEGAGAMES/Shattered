using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour {
    
	new Renderer renderer;
    bool red = false;

	void Start () {
        renderer = GetComponent<Renderer>();
	}

    private void Update()
    {
        transform.position = transform.position + Vector3.zero;
    }

    public void TakeDamage(float amountOfDamage)
    {
        print("auch," + amountOfDamage + " damage recieved");
        if (red == false)
        {
            renderer.material.color = Color.red;
            red = true;
        }
        else
        {
            renderer.material.color = Color.blue;
            red = false;
        }
    }
}
