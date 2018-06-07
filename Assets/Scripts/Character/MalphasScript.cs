using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalphasScript : CharacterMovement
{

    //Serialized classes
    [System.Serializable]
    public class SkillAnimations
    {
        //Use these names to change the parameters value's of the  animator, to change the animation to it's inteded state.
        public string demonicWave = "SkillWave";
        public string psychicScream = "SkillScream";
        public string startTeleport = "isTeleporting"; //bool!
        public string endTeleport = "endTeleport";
        public string possess = "SkillPossess";
        public string barrier = "SkillBarrier";
    }
    [SerializeField]
    public SkillAnimations skillAnimations;

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
    }
    [SerializeField]
    public BasicCombatSettings basicCombatSettings;

    [Serializable]
    public class BlinkSettings
    {
        public float blinkCooldown = 1;
        public float blinkDistance = 10;
        public float blinkSpeed = 5;
    }
    [SerializeField]
    public BlinkSettings blinkSettings;

    [Serializable]
    public class SkillsSettings
    {
        public SkillSettings teleportSettings;
        public SkillSettings barrierSettings;
        public SkillSettings psychicScreamSettings;
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
    private String lastAttack;

    private bool blinking = false;
    Vector3 blinkTargetPosition;


    //check if skill is unlocked
    protected override void CharactertInitialize()
    {
        markerManager = this.transform.parent.GetComponent<MarkerManagerPlayer>();
        markerManager.SetMarkers();
        stats = this.transform.parent.GetComponent<Statistics>();

        //for development purposes
        skills.Add(new Teleport(skillSettings.teleportSettings, this));
        skills.Add(new Barrier(skillSettings.barrierSettings, stats, this));
        skills.Add(new DemonicWave(skillSettings.demonicWaveSettings, this));
        skills.Add(new PsychicScream(skillSettings.psychicScreamSettings, this));
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
        BlinkToLocation();

    }

    protected override void CombatActionUpdate()
    {
        //Select the correct action
        if (Input.GetButton(Constants.ATTACK1_BUTTON))
        {
            //prefform attack1
            attack = Attack1(animator);
            lastAttack = Constants.PUNCH_ATTACK;
            characterActionTimeStamp = Time.time + attack.cooldown;
            characterRooted = attack.rootable;

        }
        else if (Input.GetButton(Constants.ATTACK2_BUTTON))
        {
            //prefform attack2
            attack = Attack2(animator);
            lastAttack = Constants.KICK_ATTACK;
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
            if (Input.GetAxisRaw(Constants.HORIZONTAL_AXIS) != 0 || Input.GetAxisRaw(Constants.VERTICAL_AXIS) != 0)
            {
                blinkTargetPosition =  new Vector3(Input.GetAxisRaw(Constants.HORIZONTAL_AXIS) * blinkSettings.blinkDistance, 0,
                                                            Input.GetAxisRaw(Constants.VERTICAL_AXIS) * blinkSettings.blinkDistance);
                blinkTargetPosition = transform.position +  transform.TransformDirection(blinkTargetPosition);

                //check reachable
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (blinkTargetPosition - transform.position), out hit, blinkSettings.blinkDistance+1))
                {
                    blinkTargetPosition = hit.point;
                }
                
                blinking = true;
                blinkTimeStamp = Time.time + blinkSettings.blinkCooldown;
            }
        }
    }

    private void BlinkToLocation()
    {
        if (blinking == true)
        {
            var offset = blinkTargetPosition - transform.position;
            //Get the difference.


            float dist = Vector3.Distance(blinkTargetPosition, transform.position);
            if (dist > 1f)
            {
                offset = offset.normalized * 0.1f;
                characterController.Move(offset * blinkSettings.blinkSpeed);

            }else
                blinking = false;
            StartCoroutine(ForceStop());
        }
    }

    IEnumerator ForceStop()
    {
        yield return new WaitForSeconds(0.5f);
        blinking = false;
    }

    public void LearnSkill(string skill)
    {
        switch (skill)
        {
            case "Teleport":
                skills.Add(new Teleport(skillSettings.teleportSettings, this));
                break;
            case "Barrier":
                skills.Add(new Barrier(skillSettings.barrierSettings, stats, this));
                break;
            case "PsychicScream":
                skills.Add(new PsychicScream(skillSettings.psychicScreamSettings, this));
                break;
            case "DemonicWave":
                skills.Add(new DemonicWave(skillSettings.demonicWaveSettings, this));
                break;
            case "Possess":
                skills.Add(new Possess(skillSettings.possessSettings, this));
                break;
            default:
                Console.WriteLine("Skill unknown");
                break;
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

    public string GetAttackMode()
    {
        return lastAttack;
    }
}
