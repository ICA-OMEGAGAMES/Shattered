using UnityEngine;

public class TimeManager : MonoBehaviour {

	//Controll how much we slow down Time if we start the Manager
	public float slowDownFactor = 0.05f;
	//How long the slow down will occure maybee not needed
	public float slowdownDuration = 2f;

	void Update()
	{
		Time.timeScale += (1f / slowdownDuration) * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
	}

	public void DoSlowmotion()
	{
		Debug.Log("DoSlowmotion");
		Time.timeScale = slowDownFactor;
		Time.fixedDeltaTime = Time.timeScale * 0.02f;
	}
	
}
