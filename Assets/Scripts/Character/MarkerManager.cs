using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour{


    List<GameObject> markers = new List<GameObject>();

    List<GameObject> hitBySwing = new List<GameObject>();

    public void SetMarkers()
    {
        markers = FindMarkers();
    }

    private List<GameObject> FindMarkers()
    {
        Transform[] Children = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in Children)
        {
            if (child.tag == "Marker")
            {
                try
                {
                    child.gameObject.GetComponent<MarkerScript>();
                }
                catch
                {
                    child.gameObject.AddComponent<MarkerScript>();
                }
                markers.Add(child.gameObject);
            }
        }
        return markers;
    }

    public void EnableMarkers()
    {
        foreach (GameObject mark in markers)
        {
            MarkerScript markerscript = mark.GetComponent<MarkerScript>();
            markerscript.EnableHit();
        }
    }

    public void DisableMarkers()
    {
        foreach (GameObject mark in markers)
        {
            MarkerScript markerscript = mark.GetComponent<MarkerScript>();
            markerscript.DisableHit();
        }
        hitBySwing.Clear();
    }

    public void NotifyHit(GameObject hitTarget)
    {
        if (hitTarget.tag != "Enemy")
            return;
        //TODO: apply damage of the item
        if (!hitBySwing.Contains(hitTarget))
        {
            hitBySwing.Add(hitTarget.gameObject);
            hitTarget.GetComponent<DummyScript>().TakeDamage(10);
        }
    }
}
