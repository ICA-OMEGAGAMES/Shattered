using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistCollisionScript : MonoBehaviour {
    private bool collisionEnabled = false;
    public void EnableColliders()
    {
        collisionEnabled = true;
    }

    public void DisableColliders()
    {
        collisionEnabled = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
            return;
        if(enabled)
         other.GetComponent<DummyScript>().TakeDamage();        
    }
}
