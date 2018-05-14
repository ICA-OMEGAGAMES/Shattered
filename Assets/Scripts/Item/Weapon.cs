using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    public float damage;
    public DevonScript.FSMState weaponType = DevonScript.FSMState.LightMeleeWeapon;
}
