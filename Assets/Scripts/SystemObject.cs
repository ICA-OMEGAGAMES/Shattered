using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SystemObject : ScriptableObject {

    [SerializeField]
    public int current_character = 0; // 0 represents the yin charcter and 1 is the yang character

    [SerializeField]
    public float morality_value = 0;
}
