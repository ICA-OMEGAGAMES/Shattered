using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour {
    new Renderer renderer;
    bool red = false;
	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
	}

    private void Update()
    {
        transform.position = transform.position + Vector3.zero;
    }

    public void TakeDamage()
    {
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
