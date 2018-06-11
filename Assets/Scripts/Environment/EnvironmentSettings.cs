using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "Shattered/Environment")]
public class EnvironmentSettings : ScriptableObject {

	[System.Serializable]
	public class Rain
	{
		public float minimumCorruption = 25;
		public float maximumCorruption = 60;

		public float divider = 10;
		public float frequency = 100;

		public float current = 0;
	}

	[SerializeField]
	public Rain rain;

	[System.Serializable]
	public class BloodRain 
	{
		public float minimumCorruption = 40;
		public float maximumCorruption = 100;

		public float divider = 10;
		public float frequency = 50;

		public float current = 0;
	}

	[SerializeField]
	public BloodRain bloodRain;

	[System.Serializable]
	public class Fog 
	{
		public float minimumCorruption = 40;
		public float maximumCorruption = 100;

		public float divider = 10;
		public float density = 100;

		public float current = 0;
	}

	[SerializeField]
	public Fog fog;
}