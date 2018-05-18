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
        //if the hitTarget is neither the player nor the ai return
        if (hitTarget.transform.root.tag != Constants.ENEMY_TAG && hitTarget.transform.root.tag != Constants.PLAYER_TAG)
            return;

        if (!hitBySwing.Contains(hitTarget))
        {
            hitBySwing.Add(hitTarget.gameObject);

            if(hitTarget.transform.root.tag == Constants.PLAYER_TAG && transform.root.tag != Constants.PLAYER_TAG)
            {
                //if the hitTarget is on the player and the source is not the player apply damage
                hitTarget.transform.root.GetComponentInChildren<Statistics>().ReduceHealth(damage);
            } 
            else if(hitTarget.transform.root.tag == Constants.ENEMY_TAG && transform.root.tag != Constants.ENEMY_TAG)
            {
                    //if the hitTarget is on the ai and the source is not the ai apply damage
                    //TODO: Dummyscript is just for demo so the if can be removed later on
                    Component component = hitTarget.transform.root.GetComponentInChildren<AIManager>();
                    if(component == null)
                    {
                        component = hitTarget.transform.root.GetComponent<DummyScript>();
                        ((DummyScript) component).TakeDamage(damage);
                        return;
                    }
                    ((AIManager) component).TakeDamage(damage);
            }
        }
    }
}
