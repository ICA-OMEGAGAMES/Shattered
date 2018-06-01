using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour {

	public Slider healthbar;

	public float health;
    public float hitStunDuration = 0.5f;
    private float maxHealth = 100f;
    private float blocks = 0;
    private bool immune = false;
    private GameObject spawnpoint;
    public GameObject shield;
    private bool blocking = false;
    public float blockReductionPersentage = 0.50f;

    void Start(){
		maxHealth = Constants.MAX_PLAYER_HEALTH;
		health = maxHealth;
        if(shield != null)
            shield.SetActive(false);
    }

	public float GetHealth(){return health;}
	public void IncreaseMaxHealth(float amount){maxHealth += amount;}

	public void ReduceHealth (float amount){
        if (!immune)
        {
            if (blocks <= 0)
            {
                if (blocking)
                    amount = amount -(amount * blockReductionPersentage);
                else
                    Stun(hitStunDuration);
                health -= amount;

                if (health <= Constants.MIN_PLAYER_HEALTH)
                    health = Constants.MIN_PLAYER_HEALTH;
                healthbar.value = CalculateHealth();
            }
            else
            {
                blocks--;
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

    public bool Immune {
        get { return immune; }
        set { immune = value; }
    }

    public bool Blocking
    {
        get { return blocking; }
        set { blocking = value; }
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

    private void Stun(float duration)
    {
        //find component call stunn
        if (GetComponent<CharacterTransformer>().currentForm == CharacterTransformer.CharacterForm.devon)
        {
            StartCoroutine(GetComponentInChildren<DevonScript>().StunCharacter(duration));
        }
        else if (GetComponent<CharacterTransformer>().currentForm == CharacterTransformer.CharacterForm.malphas)
        {
            StartCoroutine(GetComponentInChildren<MalphasScript>().StunCharacter(duration));
        }
    }
}
