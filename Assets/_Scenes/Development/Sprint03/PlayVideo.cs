using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    MovieTexture movie;
    RawImage rawImageComp;
    AudioSource audioS;
    private bool movieStarted;

    VideoPlayer clip;

    // Use this for initialization
    void Start()
    {
        rawImageComp = GetComponent<RawImage>();
        audioS = GetComponent<AudioSource>();
        clip = GetComponent<VideoPlayer>();
        // movie = clip.texture;
        PlayClip();
    }

    void PlayClip()
    {
        audioS.clip = movie.audioClip;
        audioS.Play();
        rawImageComp.texture = movie;
        movie.Play();
        movieStarted = true; // paranoia, only necessary if Update() is called before this one     
    }

    void Update()
    {
        if (movieStarted && !movie.isPlaying)
        {
            movieStarted = false; // avoid getting here after the scene change was triggered, because it might be done asynchronously
                                  // Do something great here, probably this:
            // Application.LoadLevel("MyLevel");
        }
    }
}