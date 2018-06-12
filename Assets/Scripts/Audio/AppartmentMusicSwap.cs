using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppartmentMusicSwap : MonoBehaviour {

	public GameObject musicMuffledObject;
	public GameObject musicHeadObject;

    public AudioSource musicMuffled;
    public AudioSource musicHead;

    public static AppartmentMusicSwap instance;

	private bool appartmentMusic = true;

    void Awake () {
		musicMuffledObject = GameObject.Find(Constants.MUFFLED_AUDIOSOURCE);
		musicHeadObject = GameObject.Find(Constants.HEAD_AUDIOSOURCE);

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
		if (appartmentMusic)
		{
            musicHead.mute = !musicHead.mute;
            musicMuffled.mute = !musicHead.mute;
            appartmentMusic = false;
		}
		else
		{
            musicHead.mute = !musicHead.mute;
            musicMuffled.mute = !musicHead.mute;
            appartmentMusic = true;
		}

	}
}
