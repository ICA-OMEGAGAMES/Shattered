using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerSkills : MonoBehaviour
{


    public List<AudioClip> SFXlist;

    private AudioSource audioSource;
    private bool isPlaying;


    void Start()
    {

        audioSource = GetComponent<AudioSource>();

    }

    public void InvokePlaySoundTransform()
    {
        StartCoroutine(PlaySoundTransform());
    }

    //skills
    public void InvokePlaySoundAOESpike()
    {
        StartCoroutine(PlaySoundAOESpike());
    }

    public void InvokePlaySoundBlink()
    {
        StartCoroutine(PlaySoundBlink());
    }

    public void InvokePlaySoundPossession()
    {
        StartCoroutine(PlaySoundPossession());
    }

    public void InvokePlaySoundScream()
    {
        StartCoroutine(PlaySoundScream());
    }

    //teleport
    public void InvokePlaySoundTeleportStart()
    {
        StartCoroutine(PlaySoundTeleportStart());
    }

    public void InvokePlaySoundTeleportEnd()
    {
        StartCoroutine(PlaySoundTeleportEnd());
    }
    //barrier
    public void InvokePlaySoundBarrierStart()
    {
        StartCoroutine(PlaySoundBarrierStart());
    }

    public void InvokePlaySoundBarrierEnd()
    {
        StartCoroutine(PlaySoundBarrierEnd());
    }


    public IEnumerator PlaySoundAOESpike()
    {

        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.clip = SFXlist[0];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }
    public IEnumerator PlaySoundBlink()
    {

        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.clip = SFXlist[4];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;

        }
    }
    public IEnumerator PlaySoundPossession()
    {

        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.clip = SFXlist[5];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }
    public IEnumerator PlaySoundTransform()
    {

        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.clip = SFXlist[6];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }
    public IEnumerator PlaySoundScream()
    {

        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.clip = SFXlist[7];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }
    public IEnumerator PlaySoundTeleportStart()
    {

        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.clip = SFXlist[8];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }
    public IEnumerator PlaySoundTeleportEnd()
    {

        if (!isPlaying)
        {

            isPlaying = true;
            audioSource.clip = SFXlist[9];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }
    public IEnumerator PlaySoundBarrierStart()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.clip = SFXlist[1];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            audioSource.clip = SFXlist[2];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }
    public IEnumerator PlaySoundBarrierEnd()
    {
        audioSource.Stop();
        isPlaying = false;
        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.clip = SFXlist[3];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }
}
