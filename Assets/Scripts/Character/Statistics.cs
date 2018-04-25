using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour {

	private int maxHealth;
	private int health;

	void Start(){
		maxHealth = Constants.MAX_PLAYER_HEALTH;
		health = maxHealth;
	}

	public int GetHealth(){return health;}
	public void IncreaseMaxHealth(int amount){maxHealth += amount;}

	public void ReduceHealth (int amount){
		health -= amount;

		// TODO: implement dying action (respawn)
		if (health <= Constants.MIN_PLAYER_HEALTH) 
			health = Constants.MIN_PLAYER_HEALTH;
	}

	public void IncreaseHealth (int amount){
		health += amount;

		if (health >= maxHealth) 
			health = maxHealth;
	}


}
