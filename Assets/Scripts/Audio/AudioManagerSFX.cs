using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerSFX : MonoBehaviour {


    public List<AudioClip> SFXlist;

    private AudioSource audioSource;
    private bool isPlaying;
  

    void Start () {
        
        audioSource = GetComponent<AudioSource>();

    }

    public void InvokePlaySoundClosingDoor()
    {
        StartCoroutine(PlaySoundClosingDoor());
    }

public void InvokePlaySoundHittingBottle()
    {
    StartCoroutine(PlaySoundHittingBottle());
}

public IEnumerator PlaySoundClosingDoor()
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

    public IEnumerator PlaySoundHittingBottle()
    {

        if (!isPlaying)
        {
            isPlaying = true;
            int randomIndex = Random.Range(1, 4);
            audioSource.clip = SFXlist[randomIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            isPlaying = false;
        }
    }
}
