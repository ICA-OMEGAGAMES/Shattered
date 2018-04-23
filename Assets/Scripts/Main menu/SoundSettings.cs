using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stores the settings for the sound
public class SoundSettings {
	public float masterVolume;
	public float musicVolume;
	public float soundEffectsVolume;

	private static SoundSettings soundSettings;
	
	private SoundSettings() {}

   public static SoundSettings Instance
   {
      get 
      {
         if (soundSettings == null)
         {
            soundSettings = new SoundSettings();
         }
         return soundSettings;
      }
   }
}
