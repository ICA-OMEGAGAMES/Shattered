using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Shattered/Weapon")]
public class WeaponSettings : ItemSettings
{
    public float attack1Damage = 1;
    public float attack1Duration = 0.5f;
    public bool attack1Rootable = true;
    public float attack2Damage = 1;
    public float attack2Duration = 0.5f;
    public bool attack2Rootable = true;
    public DevonScript.FSMState weaponType = DevonScript.FSMState.LightMeleeWeapon;
}
