using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistCollisionScript : MonoBehaviour {


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
            return;
        
         other.GetComponent<DummyScript>().TakeDamage();        
    }
}
