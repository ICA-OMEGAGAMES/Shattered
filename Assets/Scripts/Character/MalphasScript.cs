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

    //Sterialized classes
    [Serializable]
    public class SkillSettings
    {
        [Serializable]
        public class Barier
        {
            public float duration = 10;
            public float cooldown = 2;
        }
        [SerializeField]
        public Barier barier;

        [Serializable]
        public class PsychicScream
        {
            public float duration = 10;
            public float cooldown = 2;
        }
        [SerializeField]
        public PsychicScream psychicScream;

        [Serializable]
        public class DivineAura
        {
            public float duration = 10;
            public float cooldown = 2;
        }
        [SerializeField]
        public DivineAura divineAura;

        [Serializable]
        public class DarkClaw
        {
            public float duration = 10;
            public float cooldown = 2;
        }
        [SerializeField]
        public DarkClaw darkClaw;

        [Serializable]
        public class DemonicWave
        {
            public float duration = 10;
            public float cooldown = 2;
        }
        [SerializeField]
        public DemonicWave demonicWave;

        [Serializable]
        public class Possess
        {
            public float duration = 10;
            public float cooldown = 2;
        }
        [SerializeField]
        public Possess possess;
    }
    [SerializeField]
    public SkillSettings skillSettings;

    public interface ISkill
    {
        void Execute(Animator animator);
    }
    public ISkill skill;

    private float blinkTimeStamp = 0;
    private CharacterAttack attack;
    private MarkerManager markerManager;
    private List<ISkill> skills = new List<ISkill>();


    //skills
    [Serializable]
    public class Barrier : ISkill
    {
        SkillSettings.Barier settings;
        public Barrier(SkillSettings settings)
        {
            this.settings = settings.barier;
        }
        public void Execute(Animator animator)
        {
            print("Barrier Used");
        }
    }

    public class PsychicScream : ISkill
    {
        SkillSettings.PsychicScream settings;
        public PsychicScream(SkillSettings settings)
        {
            this.settings = settings.psychicScream;
        }

        public void Execute(Animator animator)
        {
            print("PsychicScream Used");
        }
    }

    public class DivineAura : ISkill
    {
        SkillSettings.DivineAura settings;
        public DivineAura(SkillSettings settings)
        {
            this.settings = settings.divineAura;
        }

        public void Execute(Animator animator)
        {
            print("devineAura Used");
        }
    }

    public class DarkClaw : ISkill
    {
        SkillSettings.DarkClaw settings;
        public DarkClaw(SkillSettings settings)
        {
            this.settings = settings.darkClaw;
        }

        public void Execute(Animator animator)
        {
            print("shadowStep Used");
        }
    }

    public class DemonicWave : ISkill
    {
        SkillSettings.DemonicWave settings;
        public DemonicWave(SkillSettings settings)
        {
            this.settings = settings.demonicWave;
        }

        public void Execute(Animator animator)
        {
            print("demonicWave Used");
        }
    }

    public class Possess : ISkill
    {
        SkillSettings.DemonicWave settings;
        public Possess(SkillSettings settings)
        {
            this.settings = settings.demonicWave;
        }

        public void Execute(Animator animator)
        {
            print("possess Used");
        }
    }

    //check if skill is unlocked
    protected override void CharactertInitialize()
    {
        markerManager = this.transform.parent.GetComponent<MarkerManager>();
        markerManager.SetMarkers();

        //for development purposes
        skills.Add(new Barrier(skillSettings));
        skills.Add(new PsychicScream(skillSettings));
        skills.Add(new DivineAura(skillSettings));
        skills.Add(new DarkClaw(skillSettings));
        skills.Add(new DemonicWave(skillSettings));
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
            if (skills.Count >= 1)
                skills[0].Execute(animator);
        }
        if (Input.GetButton(Constants.SKILL2_BUTTON))
        {
            if (skills.Count >= 2) 
                skills[1].Execute(animator);
        }
        if (Input.GetButton(Constants.SKILL3_BUTTON))
        {
            if (skills.Count >= 3)
                skills[2].Execute(animator);
        }
        if (Input.GetButton(Constants.SKILL4_BUTTON))
        {
            if (skills.Count >= 4)
                skills[3].Execute(animator);
        }
        if (Input.GetButton(Constants.SKILL5_BUTTON))
        {
            if (skills.Count >= 5)
                skills[4].Execute(animator);
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

    public void LearnSkill(string skil)
    {
        //select correct skill, add to list
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
