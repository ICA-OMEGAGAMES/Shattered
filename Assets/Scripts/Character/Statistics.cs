using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour {

	public Slider healthbar;

	public float health;
    private float maxHealth = 100f;
    private float blocks = 0;
    private bool imume = false;
    private GameObject spawnpoint;
    public GameObject shield;

    void Start(){
		maxHealth = Constants.MAX_PLAYER_HEALTH;
		health = maxHealth;
        shield.SetActive(false);
    }

	public float GetHealth(){return health;}
	public void IncreaseMaxHealth(float amount){maxHealth += amount;}

	public void ReduceHealth (float amount){
        if (!imume)
        {
            if (blocks <= 0)
            {
                health -= amount;

                if (health <= Constants.MIN_PLAYER_HEALTH)
                    health = Constants.MIN_PLAYER_HEALTH;
                healthbar.value = CalculateHealth();
            }
            else
            {
                blocks--;
                print(amount + "damage blocked");
                if (blocks <= 0)
                    ActivateShield(false);
            }
        }
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

    public bool Imume {
        get { return imume; }
        set { imume = value; }
    }

    public void SetBlocks(float amountOfBlocks)
    {
        blocks = amountOfBlocks;
        if (blocks > 0)
            ActivateShield(true);
        else
            ActivateShield(false);
    }

    private void ActivateShield(bool activation)
    {
        shield.SetActive(activation);
    }
}
