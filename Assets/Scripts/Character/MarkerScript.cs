using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerScript : MonoBehaviour {

    private bool isEnabled = false;

    private MarkerManager markerManager;

    void Start() {
        markerManager = GetManager();
        this.GetComponent<MeshRenderer>().enabled = false;
    }
    
    public void EnableHit() {
        isEnabled = true;
    }
    
    public void DisableHit() {
        isEnabled = false;
    }

    public void OnTriggerStay(Collider other)
    {
        if (isEnabled) {
            markerManager.NotifyHit(other.gameObject);
        }
    }

    private MarkerManager GetManager()
    {
        return this.transform.root.GetComponent<MarkerManager>();
    }
}
