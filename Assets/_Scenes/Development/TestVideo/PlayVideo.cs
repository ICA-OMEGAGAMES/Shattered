using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    public RawImage image;

    public VideoClip videoToPlay;

    private VideoPlayer videoPlayer;

    private AudioSource audioSource;

    private bool isStarted = false;

	public bool isPrepared = false;


    IEnumerator playVideo()
    {
        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        //We want to play from video clip not from url
        videoPlayer.source = VideoSource.VideoClip;

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

		//ShowCanvas if Prepared
		isPrepared = true;
		image.enabled = true;

        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;

        while (isStarted)
        {
            //Play Video
            videoPlayer.Play();

            //Play Sound
            audioSource.Play();
            while (videoPlayer.isPlaying)
            {
                yield return null;
            }
        }
    }

    public void startVideo()
    {
		//This is needed for waiting until Video is prepared
		image.enabled = false;
		//Maybee we need to SetActive the SkillTree here

        if (!isStarted)
        {
            StartCoroutine(playVideo());
            isStarted = true;
        }
    }

    public void stopVideo()
    {
        StopCoroutine(playVideo());
        isStarted = false;
		isPrepared = false;
    }
}