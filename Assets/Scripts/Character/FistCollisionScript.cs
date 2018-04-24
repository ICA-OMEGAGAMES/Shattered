using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistCollisionScript : MonoBehaviour {


    public void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.tag == "Enemy")
        {
            print("triggered on enemy");
            other.GetComponent<DummyScript>().ChangeColour();
        }
    }
}
