using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour {

    [System.Serializable]
    public class AnimationSettings
    {
        //Use these names to change the parameters value's of the  animator, to change the animation to it's inteded state.
        public string isInCombat = "isInCombat";
        public string punch = "Punch";
        public string kick = "Kick";

    }
    [SerializeField]
    public AnimationSettings animations;

    [System.Serializable]
    public class CombatSettings
    {
        //Combat settings
        public float toggleCooldown;
        public float punchCooldown;
        public float kickCooldown;
    }
    [SerializeField]
    public CombatSettings combat;
    
    private float timeStamp;
    private bool combatState = false;

    private Animator animator;
    private UnityEngine.CharacterController characterController;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (timeStamp <= Time.time)
        {
            if (Input.GetButton(Constants.COMBAT_BUTTON))
            {
                SwitchCombatState();
                timeStamp = Time.time + combat.toggleCooldown;
            }
            if (Input.GetButton(Constants.ATTACK1_BUTTON))
            {
                if (combatState == false)
                    SwitchCombatState();
                Attack1();
                timeStamp = Time.time + combat.punchCooldown;
            }
            if (Input.GetButton(Constants.ATTACK2_BUTTON))
            {
                if (combatState == false)
                    SwitchCombatState();
                Attack2();
                timeStamp = Time.time + combat.kickCooldown;
            }
        }
    }

    public void SwitchCombatState()
    {
        if (combatState)
            combatState = false;
        else
            combatState = true;

        animator.SetBool(animations.isInCombat, combatState);
        print(combatState);
    }

    private void Attack1() {
        animator.SetTrigger(animations.punch);
    }
    private void Attack2()
    {
        animator.SetTrigger(animations.kick);
    }
}
