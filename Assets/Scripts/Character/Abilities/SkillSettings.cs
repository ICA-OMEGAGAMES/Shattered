using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shattered/Skill")]
public class SkillSettings : ScriptableObject {

    public float duration = 10;
    public float cooldown = 2;
    public float value = 1;
    public GameObject skillEffect;
}