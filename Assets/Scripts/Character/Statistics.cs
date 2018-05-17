using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour {

	public Slider healthbar;
    public GameObject spawnpoint;

	private float maxHealth = 100f;
	public float health;

	void Start(){
		maxHealth = Constants.MAX_PLAYER_HEALTH;
		health = maxHealth;
	}

	public float GetHealth(){return health;}
	public void IncreaseMaxHealth(float amount){maxHealth += amount;}

	public void ReduceHealth (float amount){
		health -= amount;

		// TODO: implement dying action (respawn)
        if (health <= Constants.MIN_PLAYER_HEALTH) 
			health = Constants.MIN_PLAYER_HEALTH;
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

    public GameObject Spawnpoint
    {
        get { return spawnpoint; }
        set { spawnpoint = value; }
    }
}
