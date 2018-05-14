using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MarkerManager))]
public class DevonScript : CharacterMovement
{

    //Sterialized classes
    [Serializable]
    public class UnarmedCombatSettings
    {
        public float attack1Duration = 0.5f;
        public bool attack1Rootable = true;
        public float attack1Damage = 10;
        public float attack2Duration = 0.5f;
        public bool attack2Rootable = true;
        public float attack2Damage = 20;
    }
    [SerializeField]
    public UnarmedCombatSettings unarmedCombatSettings;

    [Serializable]
    public class LightMeleeCombatSettings
    {
        public float attack1Duration = 0.5f;
        public bool attack1Rootable = true;
        public float attack1Damage = 10;
        public float attack2Duration = 0.5f;
        public bool attack2Rootable = true;
        public float attack2Damage = 20;
    }
    [SerializeField]
    public LightMeleeCombatSettings lightMeleeCombatSettings;

    [Serializable]
    public class HeavyMeleeCombatSettings
    {
        public float attack1Duration = 0.5f;
        public bool attack1Rootable = true;
        public float attack1Damage = 10;
        public float attack2Duration = 0.5f;
        public bool attack2Rootable = true;
        public float attack2Damage = 20;
    }
    [SerializeField]
    public HeavyMeleeCombatSettings heavyMeleeCombatSettings;


    //State machine for the different combat sets
    public enum FSMState
    {
        Unarmed,
        LightMeleeWeapon, //knifes and daggers
        HeavyMeleeWeapon, //bats and maces
    }
    public FSMState curCombatSet = FSMState.Unarmed;

    //switch for the different combat systems
    private ICombatSet SetCombatSet(Animator animator, AnimationSettings animations)
    {
        switch (curCombatSet)
        {
            case FSMState.Unarmed:
                animator.SetInteger(animations.weaponSet, 0);
                return new UnarmedCombat(animator, animations, unarmedCombatSettings);
            case FSMState.LightMeleeWeapon:
                animator.SetInteger(animations.weaponSet, 1);
                return new LightMeleeCombat(animator, animations, lightMeleeCombatSettings);
            case FSMState.HeavyMeleeWeapon:
                animator.SetInteger(animations.weaponSet, 2);
                return new HeavyMeleeCombat(animator, animations, heavyMeleeCombatSettings);
            default:
                animator.SetInteger(animations.weaponSet, 0);
                return new UnarmedCombat(animator, animations, unarmedCombatSettings);
        }
    }

    //interface for the different combat sets
    public interface ICombatSet
    {
        CharacterAttack Attack1(Animator animator);
        CharacterAttack Attack2(Animator animator);
    }
    public ICombatSet combatSet;

    /// <summary>
    /// Unarmed combat set
    /// </summary>
    public class UnarmedCombat : ICombatSet
    {
        public UnarmedCombatSettings combatSettings;
        private AnimationSettings animations;


        public UnarmedCombat(Animator animator, AnimationSettings animations, UnarmedCombatSettings combatSettings)
        {
            this.animations = animations;
            this.combatSettings = combatSettings;
        }

        public CharacterAttack Attack1(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.punch);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.attack1Damage, combatSettings.attack1Duration, combatSettings.attack1Rootable);
        }

        public CharacterAttack Attack2(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.kick);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.attack2Damage, combatSettings.attack1Duration, combatSettings.attack2Rootable);
        }
    }
    /// <summary>
    /// LightMelee combat set
    /// </summary>
    public class LightMeleeCombat : ICombatSet
    {
        public LightMeleeCombatSettings combatSettings;
        private AnimationSettings animations;

        public LightMeleeCombat(Animator animator, AnimationSettings animations, LightMeleeCombatSettings combatSettings)
        {
            this.animations = animations;
            this.combatSettings = combatSettings;
        }

        public CharacterAttack Attack1(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.punch);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.attack1Damage, combatSettings.attack2Duration, combatSettings.attack1Rootable);
        }

        public CharacterAttack Attack2(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.punch);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.attack2Damage, combatSettings.attack1Duration, combatSettings.attack2Rootable);
        }
    }
    /// <summary>
    /// HeavyMelee combat set
    /// </summary>
    public class HeavyMeleeCombat : ICombatSet
    {
        public HeavyMeleeCombatSettings combatSettings;
        private AnimationSettings animations;

        public HeavyMeleeCombat(Animator animator, AnimationSettings animations, HeavyMeleeCombatSettings combatSettings)
        {
            this.animations = animations;
            this.combatSettings = combatSettings;
        }

        public CharacterAttack Attack1(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.kick);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.attack1Damage, combatSettings.attack1Duration, combatSettings.attack1Rootable);
        }

        public CharacterAttack Attack2(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.kick);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.attack2Damage, combatSettings.attack2Duration, combatSettings.attack2Rootable);
        }
    }
    //add other combat sets here

    CharacterAttack attack;
    MarkerManager markerManager;

    //combatStart
    protected override void CombatInitialize()
    {
        combatSet = SetCombatSet(animator, animations);
        markerManager = GetComponent<MarkerManager>();
        markerManager.SetMarkers();
    }

    // combatUpdate seperatly so the combatactions are only checked when inteded
    protected override void CombatActionUpdate()
    {
        //Select the correct action
        if (Input.GetButton(Constants.ATTACK1_BUTTON))
        {
            attack = combatSet.Attack1(animator);
            characterActionTimeStamp = Time.time + attack.cooldown;
            characterRooted = attack.rootAble;
        }
        else if (Input.GetButton(Constants.ATTACK2_BUTTON))
        {
            attack = combatSet.Attack2(animator);
            characterActionTimeStamp = Time.time + attack.cooldown;
            characterRooted = attack.rootAble;
        }
    }

    //TODO: read picked up item and set type
    protected override void ChangeCombatSet()
    {

        //select the correct combatset
        if (Input.GetButton("UnarmedSet"))
        {
            curCombatSet = FSMState.Unarmed;
        }
        if (Input.GetButton("ArmedSet"))
        {
            curCombatSet = FSMState.LightMeleeWeapon;
        }
        combatSet = SetCombatSet(animator, animations);
       //markerManager.SetMarkers();
    }
}
