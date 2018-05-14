using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerScript : MonoBehaviour {

    private bool isEnabled = false;

    private MarkerManager markerManager;

    List<GameObject> hitBySwing = new List<GameObject>();

    void Start() {
        markerManager = GetManager();
    }

    // Use this for initialization
    public void EnableHit() {
        isEnabled = true;
    }

    // Update is called once per frame
    public void DisableHit() {
        isEnabled = false;
        hitBySwing.Clear();
    }

    public void OnTriggerStay(Collider other)
    {
        if (isEnabled && !hitBySwing.Contains(other.gameObject)) {
            markerManager.NotifyHit(other.gameObject);
            hitBySwing.Add(other.gameObject);
        }
    }

    private MarkerManager GetManager()
    {
        return this.transform.root.GetComponent<MarkerManager>();
    }
}
