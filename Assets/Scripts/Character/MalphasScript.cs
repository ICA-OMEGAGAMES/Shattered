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
    //Make interface for the different ability's.
    //check which skill need to trigger upon corruption level
    //check if skill is unlocked
    protected override void CharactertInitialize()
    {
        
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

        }
        else if (Input.GetButton(Constants.ATTACK2_BUTTON))
        {
            //prefform attack2
        }
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
}
