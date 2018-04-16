using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class ObjectiveScriptableObject : ScriptableObject
{
    [SerializeField]
    public string[] objectives;
}
