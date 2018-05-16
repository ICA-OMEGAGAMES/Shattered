using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalphusScript : CharacterMovement {


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
        //press Q
        //press E
        //press R
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
