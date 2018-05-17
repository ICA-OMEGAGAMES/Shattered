using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalphasScript : CharacterMovement {

    //Sterialized classes
    [Serializable]
    public class BasicCombatSettings
    {
        public float attack1Damage = 10;
        public float attack1Duration = 0.5f;
        public bool attack1Rootable = true;
        public float attack2Damage = 20;
        public float attack2Duration = 0.5f;
        public bool attack2Rootable = true;
        public float blinkCooldown = 1;
        public float blinkDistance = 2;
    }
    [SerializeField]
    public BasicCombatSettings basicCombatSettings;

    
    private float blinkTimeStamp = 0;
    private CharacterAttack attack;
    private MarkerManager markerManager;
    //Make interface for the different ability's.
    //check which skill need to trigger upon corruption level
    //check if skill is unlocked
    protected override void CharactertInitialize()
    {
        markerManager = this.transform.parent.GetComponent<MarkerManager>();
        markerManager.SetMarkers();
    }

    protected override void CharacterOutOfCombatUpdate()
    {
        //press aim teleport
        //press teleport
    }

    protected override void CharacterInCombatUpdate()
    {
        //special skills
        if (Input.GetButton(Constants.SKILL1_BUTTON))
        {
        }
        if (Input.GetButton(Constants.SKILL2_BUTTON))
        {
        }
        if (Input.GetButton(Constants.SKILL3_BUTTON))
        {
        }
        if (Input.GetButton(Constants.SKILL4_BUTTON))
        {
        }
        if (Input.GetButton(Constants.SKILL5_BUTTON))
        {
        }
    }

    protected override void CharacterOutOfCombatFixedUpdate()
    {
    }

    protected override void CharacterInCombatFixedUpdate()
    {
        if (Input.GetButton(Constants.DODGE_BUTTON))
            Blink();
    }

    protected override void CombatActionUpdate()
    {
        //Select the correct action
        if (Input.GetButton(Constants.ATTACK1_BUTTON))
        {
            //prefform attack1
            attack = Attack1(animator);
            characterActionTimeStamp = Time.time + attack.cooldown;
            characterRooted = attack.rootAble;

        }
        else if (Input.GetButton(Constants.ATTACK2_BUTTON))
        {
            //prefform attack2
            attack = Attack2(animator);
            characterActionTimeStamp = Time.time + attack.cooldown;
            characterRooted = attack.rootAble;
        }
    }

    public CharacterAttack Attack1(Animator AM)
    {
        //proc the animation
        AM.SetTrigger(animations.attack1);
        //set the cooldown and if the character is rooted durring this skill
        return new CharacterAttack(basicCombatSettings.attack1Damage, basicCombatSettings.attack1Duration, basicCombatSettings.attack1Rootable);
    }

    public CharacterAttack Attack2(Animator AM)
    {
        //proc the animation
        AM.SetTrigger(animations.attack2);
        //set the cooldown and if the character is rooted durring this skill
        return new CharacterAttack(basicCombatSettings.attack2Damage, basicCombatSettings.attack2Duration, basicCombatSettings.attack2Rootable);
    }

    private void Blink()
    {
        if (blinkTimeStamp <= Time.time)
        {
            if (Input.GetAxis(Constants.HORIZONTAL_AXIS) != 0 || Input.GetAxis(Constants.VERTICAL_AXIS) != 0)
            {
                characterController.transform.Translate(new Vector3(Input.GetAxis(Constants.HORIZONTAL_AXIS) * basicCombatSettings.blinkDistance,0, 
                                                                    Input.GetAxis(Constants.VERTICAL_AXIS) * basicCombatSettings.blinkDistance));
                blinkTimeStamp = Time.time + basicCombatSettings.blinkCooldown;
            }
        }
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
