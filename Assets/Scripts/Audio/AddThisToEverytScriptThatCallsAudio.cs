using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManagerCombat))]
[RequireComponent(typeof(AudioManagerMovement))]
[RequireComponent(typeof(AudioManagerSFX))]
[RequireComponent(typeof(AudioManagerSkills))]


public class AddThisToEverytScriptThatCallsAudio : MonoBehaviour {

    public AudioManagerCombat audioManagerCombat;
    public AudioManagerMovement audioManagerMovement;
    public AudioManagerSFX audioManagerSFX;
    public AudioManagerSkills audioManagerSkills;

    private void Awake()
    {
        audioManagerCombat = this.GetComponent<AudioManagerCombat>();
        audioManagerMovement = this.GetComponent<AudioManagerMovement>();
        audioManagerSFX = this.GetComponent<AudioManagerSFX>();
        audioManagerSkills = this.GetComponent<AudioManagerSkills>();
    }
}
