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
    
    [Serializable]
    public class SkillsSettings
    {
        public SkillSettings teleportSettings;
        public SkillSettings barierSettings;
        public SkillSettings phychicScreamSettings;
        public SkillSettings divineAuraSettings;
        public SkillSettings darkClawSettings;
        public SkillSettings demonicWaveSettings;
        public SkillSettings possessSettings;
    }
    [SerializeField]
    public SkillsSettings skillSettings;

    private float blinkTimeStamp = 0;
    private CharacterAttack attack;
    private MarkerManagerPlayer markerManager;
    private List<ISkill> skills = new List<ISkill>();
    private Statistics stats;

    //check if skill is unlocked
    protected override void CharactertInitialize()
    {
        markerManager = this.transform.parent.GetComponent<MarkerManagerPlayer>();
        markerManager.SetMarkers();
        stats = this.transform.parent.GetComponent<Statistics>();

        //for development purposes
        skills.Add(new Teleport(skillSettings.teleportSettings, this));
        skills.Add(new Barrier(skillSettings.barierSettings, stats, this));
        skills.Add(new PhychicScream(skillSettings.phychicScreamSettings, this));
        skills.Add(new DivineAura(skillSettings.divineAuraSettings, this));
        skills.Add(new DarkClaw(skillSettings.darkClawSettings, this));
        skills.Add(new DemonicWave(skillSettings.demonicWaveSettings, this));
        skills.Add(new Possess(skillSettings.possessSettings, this));

    }

    protected override void CharacterOutOfCombatUpdate()
    {
        //todo: teleport skill
        //press aim teleport
        //press teleport
        //(possible by Iskill?)

    }

    protected override void CharacterInCombatUpdate()
    {
        //special skills
        if (Input.GetButton(Constants.SKILL1_BUTTON))
        {
            if (skills.Count >= 1)
                skills[1].Execute(animator);
        }
        if (Input.GetButton(Constants.SKILL2_BUTTON))
        {
            if (skills.Count >= 2) 
                skills[2].Execute(animator);
        }
        if (Input.GetButton(Constants.SKILL3_BUTTON))
        {
            if (skills.Count >= 3)
                skills[3].Execute(animator);
        }
        if (Input.GetButton(Constants.SKILL4_BUTTON))
        {
            if (skills.Count >= 4)
                skills[4].Execute(animator);
        }
        if (Input.GetButton(Constants.SKILL5_BUTTON))
        {
            if (skills.Count >= 5)
                skills[5].Execute(animator);
        }
    }

    protected override void CharacterOutOfCombatFixedUpdate()
    {
        //Malphas out of combat fixed updates
    }

    protected override void CharacterInCombatFixedUpdate()
    {
        //Malphas in of combat fixed updates
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
            characterRooted = attack.rootable;

        }
        else if (Input.GetButton(Constants.ATTACK2_BUTTON))
        {
            //prefform attack2
            attack = Attack2(animator);
            characterActionTimeStamp = Time.time + attack.cooldown;
            characterRooted = attack.rootable;
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

    public void LearnSkill(string skill)
    {
        //TODO: learnSkill from skilltree
        //skills.Add();
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
