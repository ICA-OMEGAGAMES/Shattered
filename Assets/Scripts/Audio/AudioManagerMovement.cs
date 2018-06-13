using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMovement : MonoBehaviour {

	public List<AudioClip> walkingDevon;
    public List<AudioClip> walkingDevonOutside;
    public List<AudioClip> walkingMalphas;

    public bool isInside;

    public bool isMalphas = false;
	public bool isRunning = false;
    public bool isCrouching = false;
    public bool isInCombat = false;

	public float walkingAudioOffset;
	public float runningAudioOffset;
	public float crouchingAudioOffset;
	public float combatAudioOffset;


	private AudioSource audioSource;
	private bool walkingSoundsPlaying = false;




	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	public void InvokeWalkingSoundsCoroutine(){
		if (!walkingSoundsPlaying && isRunning)
		{
            if (!isMalphas)
            {
                StartCoroutine(PlayWalkingSounds());
            }
            else
            {
                StartCoroutine(PlayWalkingSoundsMalphas());
            }
		}

		else if (!walkingSoundsPlaying)
				{
			StartCoroutine(PlayRunningSounds());
		}
        else if (!walkingSoundsPlaying && isCrouching)
        {
            PlaycrouchingSounds();            
        }

        else if (!walkingSoundsPlaying && isInCombat)
        {
            PlayCombatSounds();
        }
    }

	IEnumerator PlayWalkingSounds() {
		walkingSoundsPlaying = true;

		int randIndex = Random.Range(0, walkingDevon.Count);
        if (isInside == true)
        {
            if (audioSource.clip == walkingDevon[randIndex])
            randIndex = Random.Range(0, walkingDevon.Count);
            audioSource.clip = walkingDevon[randIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + walkingAudioOffset);
            walkingSoundsPlaying = false;
        }
        if(isInside == false)
        {
            if (audioSource.clip == walkingDevonOutside[randIndex])
            randIndex = Random.Range(0, walkingDevonOutside.Count);
            audioSource.clip = walkingDevonOutside[randIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + walkingAudioOffset);
            walkingSoundsPlaying = false;
        }
    }

	IEnumerator PlayRunningSounds()
	{
        walkingSoundsPlaying = true;

        int randIndex = Random.Range(0, walkingDevon.Count);
        if (isInside == true)
        {
            if (audioSource.clip == walkingDevon[randIndex])
                randIndex = Random.Range(0, walkingDevon.Count);
            audioSource.clip = walkingDevon[randIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + runningAudioOffset);
            walkingSoundsPlaying = false;
        }
        if (isInside == false)
        {
            if (audioSource.clip == walkingDevonOutside[randIndex])
                randIndex = Random.Range(0, walkingDevonOutside.Count);
            audioSource.clip = walkingDevonOutside[randIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + runningAudioOffset);
            walkingSoundsPlaying = false;
        }
    }

	IEnumerator PlayCombatSounds()
	{
        walkingSoundsPlaying = true;

        int randIndex = Random.Range(0, walkingDevon.Count);
        if (isInside == true)
        {
            if (audioSource.clip == walkingDevon[randIndex])
                randIndex = Random.Range(0, walkingDevon.Count);
            audioSource.clip = walkingDevon[randIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + combatAudioOffset);
            walkingSoundsPlaying = false;
        }
        if (isInside == false)
        {
            if (audioSource.clip == walkingDevonOutside[randIndex])
                randIndex = Random.Range(0, walkingDevonOutside.Count);
            audioSource.clip = walkingDevonOutside[randIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + combatAudioOffset);
            walkingSoundsPlaying = false;
        }
    }

	IEnumerator PlaycrouchingSounds()
	{
        walkingSoundsPlaying = true;

        int randIndex = Random.Range(0, walkingDevon.Count);
        if (isInside == true)
        {
            if (audioSource.clip == walkingDevon[randIndex])
                randIndex = Random.Range(0, walkingDevon.Count);
            audioSource.clip = walkingDevon[randIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + crouchingAudioOffset);
            walkingSoundsPlaying = false;
        }
        if (isInside == false)
        {
            if (audioSource.clip == walkingDevonOutside[randIndex])
                randIndex = Random.Range(0, walkingDevonOutside.Count);
            audioSource.clip = walkingDevonOutside[randIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + crouchingAudioOffset);
            walkingSoundsPlaying = false;
        }
    }

    IEnumerator PlayWalkingSoundsMalphas()
    {
        walkingSoundsPlaying = true;

        int randIndex = Random.Range(0, walkingMalphas.Count);
        if (audioSource.clip == walkingMalphas[randIndex])
            randIndex = Random.Range(0, walkingMalphas.Count);
        audioSource.clip = walkingMalphas[randIndex];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + walkingAudioOffset);
        walkingSoundsPlaying = false;
    }

    IEnumerator PlayRunningSoundsMalphas()
    {
        walkingSoundsPlaying = true;

        int randIndex = Random.Range(0, walkingMalphas.Count);
        if (audioSource.clip == walkingMalphas[randIndex])
            randIndex = Random.Range(0, walkingMalphas.Count);
        audioSource.clip = walkingMalphas[randIndex];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + runningAudioOffset);
        walkingSoundsPlaying = false;
    }

    IEnumerator PlayCombatSoundsMalphas()
    {
        walkingSoundsPlaying = true;

        int randIndex = Random.Range(0, walkingMalphas.Count);
        if (audioSource.clip == walkingMalphas[randIndex])
            randIndex = Random.Range(0, walkingMalphas.Count);
        audioSource.clip = walkingMalphas[randIndex];
        audioSource.volume = .5f;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + combatAudioOffset);
        audioSource.volume = 1f;
        walkingSoundsPlaying = false;
    }

    IEnumerator PlaycrouchingSoundsMalphas()
    {
        walkingSoundsPlaying = true;

        int randIndex = Random.Range(0, walkingMalphas.Count);
        if (audioSource.clip == walkingMalphas[randIndex])
            randIndex = Random.Range(0, walkingMalphas.Count);
        audioSource.clip = walkingMalphas[randIndex];
        audioSource.volume = .5f;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + crouchingAudioOffset);
        audioSource.volume = 1f;
        walkingSoundsPlaying = false;
    }

}
