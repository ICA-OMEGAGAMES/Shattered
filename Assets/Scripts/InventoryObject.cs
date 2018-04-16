using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryObject : ScriptableObject
{
    // for demo purpose only
    [SerializeField]
    public bool has_key = false;

    [SerializeField]
    public int ammo = 0;

    [SerializeField]
    public GameObject[] guns;

    [SerializeField]
    public GameObject[] melee_weapons;

    [SerializeField]
    public GameObject[] pickups;

}
