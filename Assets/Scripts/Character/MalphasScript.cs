using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalphasScript : CharacterMovement {


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
}
