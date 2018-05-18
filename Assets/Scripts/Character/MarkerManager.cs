using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour{

    List<GameObject> markers = new List<GameObject>();
    List<GameObject> hitBySwing = new List<GameObject>();

    public void SetMarkers()
    {
        markers.Clear();
        markers = FindMarkers();
    }

    private List<GameObject> FindMarkers()
    {
        Transform[] Children = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in Children)
        {
            if (child.tag == Constants.MARKER_TAG)
            {
                try
                {
                    child.gameObject.GetComponent<MarkerScript>();
                }
                catch
                {
                    child.gameObject.AddComponent<MarkerScript>();
                }
                child.gameObject.GetComponent<MarkerScript>().SetMarkerManager();
                markers.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
        return markers;
    }

    public void EnableMarkers(float damage)
    {
        foreach (GameObject mark in markers)
        {
            mark.SetActive(true);
            MarkerScript markerscript = mark.GetComponent<MarkerScript>();
            markerscript.EnableHit(damage);
        }
    }

    public void DisableMarkers()
    {
        foreach (GameObject mark in markers)
        {
            MarkerScript markerscript = mark.GetComponent<MarkerScript>();
            markerscript.DisableHit();
            mark.SetActive(false);
        }
        hitBySwing.Clear();
    }

    public void NotifyHit(GameObject hitTarget, float damage)
    {
        if (hitTarget.tag != Constants.ENEMY_TAG)
            return;

        if (!hitBySwing.Contains(hitTarget))
        {
            hitBySwing.Add(hitTarget.gameObject);
            hitTarget.GetComponent<DummyScript>().TakeDamage(damage);
        }
    }
}
