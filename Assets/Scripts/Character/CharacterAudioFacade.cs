using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioFacade : MonoBehaviour {

    public AudioManagerSkills skills;
    public AudioManagerCombat combat;

    public void PlaySoundSpikes()
    {
        skills.InvokePlaySoundAOESpike();
    }

    public void PlaySoundBlink()
    {
        skills.InvokePlaySoundBlink();
    }

    public void PlaySoundPossession()
    {
        skills.InvokePlaySoundPossession();
    }

    public void PlaySoundScream()
    {

        skills.InvokePlaySoundScream();
    }

    public void PlaySoundTeleportStart()
    {

        skills.InvokePlaySoundTeleportStart();
    }

    public void PlaySoundTeleportEnd()
    {

        skills.InvokePlaySoundTeleportEnd();
    }

    public void PlaySoundTransformToMalphas()
    {
        skills.InvokePlaySoundTransform();
    }

    public void PlaySoundDeathDevon()
    {
        combat.InvokePlayDeathSoundDevon();
    }

    public void PlaySoundDeathMalphas()
    {
        combat.InvokePlayDeathSoundMalphas();
    }

    public void PlaySoundHitDevon()
    {
        combat.InvokePlayHitSoundDevon();
    }

    public void PlaySoundHitMalphas()
    {

        combat.InvokePlayHitSoundMalphas();
    }

    public void PlaySoundSwingBaseballbat()
    {

        combat.InvokePlaySwingSoundBaseballbat();
    }

    public void PlaySoundSwingKnife()
    {
        combat.InvokePlaySwingSoundKnife();
    }

}
