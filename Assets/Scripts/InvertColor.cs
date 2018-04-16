using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertColor : MonoBehaviour {

    public Shader invert;
    public SystemObject settings;
    public Camera main_camera;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (settings.current_character == 1)
        {
            main_camera.RenderWithShader(invert, "RenderType");
            Debug.Log("Inverted");
        }
	}
}
