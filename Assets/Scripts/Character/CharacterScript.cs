using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterScript : CharacterMovement {

    //Sterialized classes
    [Serializable]
    public class UnarmedCombatSettings
    {
        public float punshDuration = 0.5f;
        public float kickDuration = 0.5f;
        public bool rootAble = true;
    }
    [SerializeField]
    public UnarmedCombatSettings unarmedCombatSettings;

    [Serializable]
    public class LightMeleeCombatSettings
    {
        public float stabDuration = 0.5f;
        public float slashDuration = 0.5f;
        public bool rootAble = true;
    }
    [SerializeField]
    public LightMeleeCombatSettings lightMeleeCombatSettings;

    [Serializable]
    public class HeavyMeleeCombatSettings
    {
        public float hitDuration = 1;
        public float smashDuration = 2f;
        public bool rootAble = true;
    }
    [SerializeField]
    public HeavyMeleeCombatSettings heavyMeleeCombatSettings;


    //State machine for the different combat sets
    public enum FSMState
    {
        Unarmed,
        LightMeleeWeapon,
        HeavyMeleeWeapon,
        LightGun,
        HeavyGun,
    }
    public FSMState curCombatSet = FSMState.Unarmed;

    //switch for the different combat systems
    private ICombatSet setCombatSet(Animator animator, AnimationSettings animations)
    {
        switch (curCombatSet)
        {
            case FSMState.Unarmed:
                return new UnarmedCombat(animator, animations, unarmedCombatSettings);
            case FSMState.LightMeleeWeapon:
                return new LightMeleeCombat(animator, animations, lightMeleeCombatSettings);
            case FSMState.HeavyMeleeWeapon:
                return new HeavyMeleeCombat(animator, animations, heavyMeleeCombatSettings);
            default:
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
        private Animator animator;

        public UnarmedCombat(Animator animator, AnimationSettings animations, UnarmedCombatSettings combatSettings)
        {
            this.animator = animator;
            this.animations = animations;
            this.combatSettings = combatSettings;
        }

        public CharacterAttack Attack1(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.punch);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.punshDuration, combatSettings.rootAble);
        }

        public CharacterAttack Attack2(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.kick);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.punshDuration, combatSettings.rootAble);
        }
    }
    /// <summary>
    /// LightMelee combat set
    /// </summary>
    public class LightMeleeCombat : ICombatSet
    {
       public LightMeleeCombatSettings combatSettings;
        private AnimationSettings animations;
        private Animator animator;

        public LightMeleeCombat(Animator animator, AnimationSettings animations, LightMeleeCombatSettings combatSettings)
        {
            this.animator = animator;
            this.animations = animations;
            this.combatSettings = combatSettings;
        }

        public CharacterAttack Attack1(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.punch);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.slashDuration, combatSettings.rootAble);
        }

        public CharacterAttack Attack2(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.punch);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.stabDuration, combatSettings.rootAble);
        }
    }
    //add other combat sets here
    /// <summary>
    /// HeavyMelee combat set
    /// </summary>
    public class HeavyMeleeCombat : ICombatSet
    {
        public HeavyMeleeCombatSettings combatSettings;
        private AnimationSettings animations;
        private Animator animator;

        public HeavyMeleeCombat(Animator animator, AnimationSettings animations, HeavyMeleeCombatSettings combatSettings)
        {
            this.animator = animator;
            this.animations = animations;
            this.combatSettings = combatSettings;
        }

        public CharacterAttack Attack1(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.kick);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.hitDuration, combatSettings.rootAble);
        }

        public CharacterAttack Attack2(Animator AM)
        {
            //proc the animation
            AM.SetTrigger(animations.kick);
            //set the cooldown and if the character is rooted durring this skill
            return new CharacterAttack(combatSettings.smashDuration, combatSettings.rootAble);
        }
    }

    //combatStart
    protected override void CombatInitialize()
    {
        combatSet = setCombatSet(animator, animations);
    }

    // combatUpdate seperatly so the combatactions are only checked when inteded
    protected override void CombatActionUpdate()
    {
        //Select the correct action
        if (Input.GetButton(Constants.ATTACK1_BUTTON))
        {
            CharacterAttack attack= combatSet.Attack1(animator);
            characterActionTimeStamp = Time.time + attack.cooldown;
            characterRooted = attack.rootAble;
        }
        else if (Input.GetButton(Constants.ATTACK2_BUTTON))
        {
            CharacterAttack attack = combatSet.Attack1(animator);
            characterActionTimeStamp = Time.time + attack.cooldown;
            characterRooted = attack.rootAble;
        }
    }

    //constant update of the CombatSet
    protected override void CombatSetUpdate()
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
        combatSet = setCombatSet(animator,animations);
    }
}
