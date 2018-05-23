using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject[] sceneCanvase;

	private static CanvasManager instance;

    public static CanvasManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CanvasManager();
            }
            return instance;
        }
    }

    void Start()
    {
		// Disable all Canvase, because of the Event Manager
		// If two are active it can cause problems..
        for (int i = 0; i < sceneCanvase.Length; i++)
        {
			sceneCanvase[i].SetActive(false);
        }
    }
}
