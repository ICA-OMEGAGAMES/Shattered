using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour {

	private int maxHealth;
	private int health;

	void Start(){health = maxHealth;}

	public int GetHealth(){return health;}
	public void IncreaseMaxHealth(int amount){maxHealth += amount;}

	public void ReduceHealth (int amount){
		health -= amount;

		// TODO: implement dying action (respawn)
		if (health <= 0) 
			health = 0;
	}

	public void IncreaseHealth (int amount){
		health += amount;

		if (health >= maxHealth) 
			health = maxHealth;
	}


}
