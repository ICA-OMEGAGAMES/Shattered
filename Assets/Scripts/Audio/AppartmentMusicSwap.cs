using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppartmentMusicSwap : MonoBehaviour {

	public GameObject musicMuffledObject;
	public GameObject musicHeadObject;

    public AudioSource musicMuffled;
    public AudioSource musicHead;

    public static AppartmentMusicSwap instance;

    private bool AppartmentMusic = true;

    // Use this for initialization
    void Awake () {
        musicMuffledObject = GameObject.Find("MusicMuffled");
        musicHeadObject = GameObject.Find("MusicHead");

        musicMuffled = musicMuffledObject.GetComponent<AudioSource>();
        musicHead = musicHeadObject.GetComponent<AudioSource>();

        instance = this;
    }

    void Start()
    {
        musicHead.mute = true;
    }

	public void ChangeMusic()
	{
		//change music
		
		if (AppartmentMusic == true)
		{
            musicHead.mute = !musicHead.mute;
            musicMuffled.mute = !musicHead.mute;
            AppartmentMusic = false;
		}
		else
		{
            musicHead.mute = !musicHead.mute;
            musicMuffled.mute = !musicHead.mute;
            AppartmentMusic = true;
		}

	}
}
