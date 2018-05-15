using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public WeaponSettings weapon;

    public string GetName() { return weapon.itemName; }
    public string GetDescription() { return weapon.itemDescription; }
    public Image GetImage() { return weapon.image; }
    public bool CanBePickedUp() { return weapon.interactable; }

    public float Attack1Damage
    {
        get{  return weapon.attack1Damage;   }
    }

    public float Attack1Duration
    {
        get{ return weapon.attack1Duration; }
    }
    
    public bool Attack1Rootable
    {
        get { return weapon.attack1Rootable; }
    }

    public float Attack2Damage
    {
        get { return weapon.attack2Damage; }
    }

    public float Attack2Duration
    {
        get { return weapon.attack2Duration; }
    }
    public bool Attack2Rootable
    {
        get { return weapon.attack2Rootable; }
    }
    public DevonScript.FSMState WeaponType
    {
        get { return weapon.weaponType; }
    }
}

