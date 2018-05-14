using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour {

	public Slider healthbar;

	private float maxHealth = 100f;
	private float health;

	void Start(){
		health = maxHealth;

		healthbar.value = CalculateHealth ();
	}

	public float GetHealth(){return health;}
	public void IncreaseMaxHealth(float amount){maxHealth += amount;}

	public void ReduceHealth (float amount){
		health -= amount;

		// TODO: implement dying action (respawn)
		if (health <= 0) 
			health = 0;

		healthbar.value = CalculateHealth();
	}

	public void IncreaseHealth (float amount){
		health += amount;

		if (health >= maxHealth) 
			health = maxHealth;

		healthbar.value = CalculateHealth();
	}

	private float CalculateHealth(){
		return health / maxHealth;
	}


}
