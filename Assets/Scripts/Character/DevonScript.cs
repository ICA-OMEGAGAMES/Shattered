﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryController))]
[RequireComponent(typeof(Yarn.Unity.Shattered.DialoguePlayer))]
public class DevonScript : CharacterMovement
{

    //Sterialized classes
    [Serializable]
    public class UnarmedCombatSettings
    {
        public float attack1Damage = 10;
        public float attack1Duration = 0.5f;
        public bool attack1Rootable = true;
        public float attack2Damage = 20;
        public float attack2Duration = 0.5f;
        public bool attack2Rootable = true;
    }
    [SerializeField]
    public UnarmedCombatSettings unarmedCombatSettings;

    [Serializable]
    public class DodgeSettings
    {
        public float dodgeSpeedMultiplier =2;
        public float dodgeCooldown=1;
        public float dodgeDuration = 0.2f;
    }
    [SerializeField]
    public DodgeSettings dodgeSettings;

    //State machine for the different combat sets
    public enum FSMState
    {
        Unarmed,
        LightMeleeWeapon, //knifes and daggers
        HeavyMeleeWeapon, //bats and maces
    }
    public FSMState curCombatSet = FSMState.Unarmed;

    //switch for the different combat systems
    private ICombatSet SetCombatSet(Animator animator, AnimationSettings animations, Weapon weapon)
    {
        switch (curCombatSet)
        {
            case FSMState.Unarmed:
                animator.SetInteger(animations.weaponSet, 0);
                return new UnarmedCombat(animator, animations, unarmedCombatSettings);
            case FSMState.LightMeleeWeapon:
                animator.SetInteger(animations.weaponSet, 1);
                return new LightMeleeCombat(animator, animations, weapon);
            case FSMState.HeavyMeleeWeapon:
                animator.SetInteger(animations.weaponSet, 2);
                return new HeavyMeleeCombat(animator, animations, weapon);
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
        private Weapon weapon;

        public UnarmedCombat(Animator animator, AnimationSettings animations, UnarmedCombatSettings combatSettings)
        {
            this.animations = animations;
            this.combatSettings = combatSettings;
        }

        public CharacterAttack Attack1(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.attack1);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.attack1Damage, combatSettings.attack1Duration, combatSettings.attack1Rootable);
        }

        public CharacterAttack Attack2(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.attack2);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.attack2Damage, combatSettings.attack2Duration, combatSettings.attack2Rootable);
        }
    }
    /// <summary>
    /// LightMelee combat set
    /// </summary>
    public class LightMeleeCombat : ICombatSet
    {
        private AnimationSettings animations;
        private Weapon weapon;

        public LightMeleeCombat(Animator animator, AnimationSettings animations, Weapon weapon)
        {
            this.animations = animations;
            this.weapon = weapon;
        }

        public CharacterAttack Attack1(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.attack1);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(weapon.Attack1Damage, weapon.Attack1Duration, weapon.Attack1Rootable);
        }

        public CharacterAttack Attack2(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.attack2);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(weapon.Attack2Damage, weapon.Attack2Duration, weapon.Attack2Rootable);
        }
    }
    /// <summary>
    /// HeavyMelee combat set
    /// </summary>
    public class HeavyMeleeCombat : ICombatSet
    {
        private AnimationSettings animations;
        private Weapon weapon;

        public HeavyMeleeCombat(Animator animator, AnimationSettings animations, Weapon weapon)
        {
            this.animations = animations;
            this.weapon = weapon;
        }

        public CharacterAttack Attack1(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.attack1);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(weapon.Attack1Damage, weapon.Attack1Duration, weapon.Attack1Rootable);
        }

        public CharacterAttack Attack2(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.attack2);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(weapon.Attack2Damage, weapon.Attack2Duration, weapon.Attack2Rootable);
        }
    }
    //add other combat sets here

    CharacterAttack attack;
    MarkerManagerPlayer markerManager;
    private float dodgeTimestamp;

    protected override void CharactertInitialize()
    {
        combatSet = SetCombatSet(animator, animations, null);
        markerManager = this.transform.parent.GetComponent<MarkerManagerPlayer>();
        markerManager.SetMarkers();
    }

    protected override void CharacterOutOfCombatUpdate()
    {
        //Davon outOfCombat actions update
    }

    protected override void CharacterInCombatUpdate()
    {
        if (Input.GetButton(Constants.DODGE_BUTTON))
            Dodge();
    }

    protected override void CharacterOutOfCombatFixedUpdate()
    {
        //Davon outOfCombat actions fixedupdate
    }

    protected override void CharacterInCombatFixedUpdate()
    {
        //Davon InCombat actions fixedupdate
    }

    // combatUpdate seperatly so the combatactions are only checked when inteded
    protected override void CombatActionUpdate()
    {
        //Select the correct action
        if (Input.GetButton(Constants.ATTACK1_BUTTON))
        {
            attack = combatSet.Attack1(animator);
            characterActionTimeStamp = Time.time + attack.cooldown;
            characterRooted = attack.rootable;
        }
        else if (Input.GetButton(Constants.ATTACK2_BUTTON))
        {
            attack = combatSet.Attack2(animator);
            characterActionTimeStamp = Time.time + attack.cooldown;
            characterRooted = attack.rootable;
        }
    }

    private void Dodge()
    {
        if (!dodging && dodgeTimestamp <= Time.time)
        {
            if (Input.GetAxis(Constants.HORIZONTAL_AXIS) != 0 || Input.GetAxis(Constants.VERTICAL_AXIS) != 0)
            {
                StartCoroutine(Roll(movementMultiplier));
            }
        }
    }

    IEnumerator Roll(float originalMovement)
    {
        dodgeTimestamp = dodgeSettings.dodgeCooldown + Time.time;
        dodging = true;
        movementMultiplier = dodgeSettings.dodgeSpeedMultiplier;
        yield return new WaitForSeconds(dodgeSettings.dodgeDuration);
        movementMultiplier = originalMovement;
        StartCoroutine(Root(dodgeSettings.dodgeDuration));
        dodging = false;
    }
    
    IEnumerator Root(float dodgeDuration)
    {
        characterRooted = true;
        yield return new WaitForSeconds(dodgeDuration);
        characterRooted = false;
    }

    public void ChangeCombatSet(Weapon weapon)
    {
        if(!weapon)
            curCombatSet = FSMState.Unarmed;
        else
            curCombatSet = weapon.WeaponType;
        combatSet = SetCombatSet(animator, animations, weapon);
        markerManager.SetMarkers();
    }

    public void EnableMarkers()
    {
        markerManager.EnableMarkers(attack.damage);
    }

    public void DisableMarkers()
    {
        markerManager.DisableMarkers();
    }
}
