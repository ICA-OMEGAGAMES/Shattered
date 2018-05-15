using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour {

    public GameObject currentWeapon;


    public GameObject CurrentWeapon
    {
        set { currentWeapon = CurrentWeapon;}
        get { return currentWeapon; }
    }

    public void EquipWeapon(GameObject weapon)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);
        currentWeapon = weapon;
        currentWeapon.transform.position = this.transform.position;
        currentWeapon.transform.rotation = this.transform.rotation;
        currentWeapon.transform.parent = this.transform;
        currentWeapon.transform.tag = Constants.UNTAGGED;
    }
}
