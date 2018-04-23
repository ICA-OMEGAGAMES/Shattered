using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(UnityEngine.CharacterController))]
[RequireComponent(typeof(CharacterScript))]
public class CombatHandler : MonoBehaviour {
    //sterialized classes
    [System.Serializable]
    public class AnimationSettings
    {
        public string punch = "Punch";
        public string kick = "Kick";
        public string isInCombat = "isInCombat";

    }
    [SerializeField]
    public AnimationSettings animations;

    [System.Serializable]
    public class GeneralCombatSettings
    {
        public float toggleCombatCooldown = 1;
    }
    [SerializeField]
    public GeneralCombatSettings combatSettings;

    [System.Serializable]
    public class UnarmedCombat
    {
        public float punchCooldown = 1;
        public float kickCooldown = 1;
    }
    [SerializeField]
    public UnarmedCombat unarmedCombat;

    void Start () {
        characterScript = GetComponent<CharacterScript>();
        animator = GetComponent<Animator>();
    }
    
    private bool combatState = false;

    private CharacterScript characterScript;
    public Animator animator;

    // Update is called once per frame
    void Update () {
        if (characterScript.characterActionTimeStamp <= Time.time)
        {
            print("skil avaiable");
            if (Input.GetButton(Constants.COMBAT_BUTTON))
            {
                SwitchCombatState();
            }
            if (Input.GetButton(Constants.ATTACK1_BUTTON))
            {
                print("gopunch");
                punch();
            }
            else if (Input.GetButton(Constants.ATTACK2_BUTTON))
            {
                kick();
            }
        }
    }

    private void punch()
    {
        print("punch");
        if (combatState == false)
            SwitchCombatState();
        characterScript.characterActionTimeStamp = Time.time + unarmedCombat.punchCooldown;
        animator.SetTrigger(animations.punch);
    }

    private void kick()
    {
        print("kick");
        if (combatState == false)
            SwitchCombatState();
        characterScript.characterActionTimeStamp = Time.time + unarmedCombat.kickCooldown;
        animator.SetTrigger(animations.kick);
    }

    public void SwitchCombatState()
    {
        if (combatState)
            combatState = false;
        else
            combatState = true;

        animator.SetBool(animations.isInCombat, combatState);
        characterScript.characterActionTimeStamp = Time.time + combatSettings.toggleCombatCooldown;
    }
}
