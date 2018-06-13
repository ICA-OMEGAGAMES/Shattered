using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerCombat : MonoBehaviour
{

    public List<AudioClip> Impact;
    public List<AudioClip> Death;
    public List<AudioClip> Swing;
    public List<AudioClip> Hit;

    private bool isPlaying;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void InvokePlayImpactSoundKnife()
    {
        StartCoroutine(PlayImpactSoundKnife());
    }

    public void InvokePlayImpactSoundBaseballbat()
    {
        StartCoroutine(PlayImpactSoundBaseballbat());
    }

    public void InvokePlayDeathSoundDevon()
    {
        StartCoroutine(PlayDeathSoundDevon());
    }

    public void InvokePlayDeathSoundMalphas()
    {
        StartCoroutine(PlayDeathSoundMalphas());
    }

    public void InvokePlaySwingSoundKnife()
    {
        StartCoroutine(PlaySwingSoundKnife());
    }

    public void InvokePlaySwingSoundBaseballbat()
    {
        StartCoroutine(PlaySwingSoundBaseballbat());
    }

    public void InvokePlayHitSoundDevon()
    {
        StartCoroutine(PlayHitSoundDevon());
    }
    public void InvokePlayHitSoundMalphas()
    {
        StartCoroutine(PlayHitSoundMalphas());
    }

    IEnumerator PlayImpactSoundKnife()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            int randomIndex = Random.Range(0, 2);
            audioSource.clip = Impact[randomIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }

    }
    IEnumerator PlayImpactSoundBaseballbat()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            int randomIndex = Random.Range(3, 7);
            audioSource.clip = Impact[randomIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }

    }

    IEnumerator PlayDeathSoundDevon()
    {

        if (!isPlaying)
        {
            isPlaying = true;
            int randomIndex = Random.Range(0, 2);
            audioSource.clip = Death[randomIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }


    }
    IEnumerator PlayDeathSoundMalphas()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            int randomIndex = Random.Range(3, 5);
            audioSource.clip = Death[randomIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }


    IEnumerator PlaySwingSoundKnife()
    {

        if (!isPlaying)
        {
            isPlaying = true;
            int randomIndex = Random.Range(0, 3);
            audioSource.clip = Swing[randomIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }

    IEnumerator PlaySwingSoundBaseballbat()
    {

        if (!isPlaying)
        {
            isPlaying = true;
            int randomIndex = Random.Range(3, 5);
            audioSource.clip = Swing[randomIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }

    IEnumerator PlayHitSoundDevon()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            int randomIndex = Random.Range(0, 7);
            audioSource.clip = Hit[randomIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }


    }
    IEnumerator PlayHitSoundMalphas()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            int randomIndex = Random.Range(7, 12);
            audioSource.clip = Hit[randomIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }
}
