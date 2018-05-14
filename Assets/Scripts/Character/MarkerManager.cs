using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour{


    List<GameObject> markers = new List<GameObject>();

    public void SetMarkers()
    {
        markers = FindMarkers();
        print(markers.Count);
    }

    private List<GameObject> FindMarkers()
    {
        Transform[] Children = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in Children)
        {
            if (child.tag == "Marker")
            {
                markers.Add(child.gameObject);
            }
        }
        return markers;
    }

    public void EnableMarkers()
    {
        print("activate");
    }

    public void DisableMarkers()
    {
        print("deactivate");
    }
}
