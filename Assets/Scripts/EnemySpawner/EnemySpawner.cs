using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] spawners;
    public GameObject enemyPrefab;
    public int rounds;
    public int[] enemiesPerRound;
    private int currentRound;
    private bool playerReady;


	// Use this for initialization
	void Start () {
		if (spawners.Length == 0)
        {
            //grab the prefab from the directory
            //and set it's position to (0,0,0) in the level
        }

        //set the current round as 0 as it is the start of the game;
        currentRound = 0;

        //set that the player isn't ready.
        playerReady = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxisRaw("PlayerReady") != 0)
        {
            playerReady = true;
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && playerReady == true)
        {
            for (int i=0; i < enemiesPerRound[currentRound]; i++)
            {
                SpawnEnemy();
            }

            playerReady = false;
        }
	}

    void SpawnEnemy()
    {
        GameObject spawnpoint = spawners[Random.Range(0, spawners.Length - 1)];
        Instantiate(enemyPrefab, spawnpoint.transform);
    }
}
