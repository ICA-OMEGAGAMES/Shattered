using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour {

    public AudioManagerSkills skills;
    public AudioManagerCombat combat;

    public void PlaySoundSpikes()
    {
        skills.InvokePlaySoundAOESpike();
    }

    public void PlaySoundBlink()
    {
        skills.PlaySoundBlink();
    }

    public void PlaySoundPossession()
    {
        skills.PlaySoundPossession();
    }

    public void PlaySoundScream()
    {

        skills.PlaySoundScream();
    }

    public void PlaySoundTeleportStart()
    {

        skills.PlaySoundTeleportStart();
    }

    public void PlaySoundTeleportEnd()
    {

        skills.PlaySoundTeleportEnd();
    }

    public void PlaySoundTransform()
    {
        skills.PlaySoundTransform();
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
