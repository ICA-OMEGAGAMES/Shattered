using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerScript : MonoBehaviour {

    private bool isEnabled = false;

    private MarkerManager markerManager;

    private float markerDamage;

    void Start() {
        SetMarkerManager();
    }

    public void SetMarkerManager()
    {
        markerManager = GetManager();
         this.GetComponent<MeshRenderer>().enabled = false;
    }
    
    public void EnableHit(float damage) {
        markerDamage = damage;
        isEnabled = true;
    }
    
    public void DisableHit() {
        isEnabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (isEnabled) {
            markerManager.NotifyHit(other.gameObject, markerDamage);
        }
    }

    private MarkerManager GetManager()
    {
        return this.transform.root.GetComponent<MarkerManager>();
    }
}
