using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    //EnemyStats
    public int detectionRange;

    public int movementSpeed;
    public SystemObject character_settings;

    public NavMeshAgent navAgent;
    protected GameObject[] pointList;

    public GameObject player;

    private EnemyStateMachine SM;

    protected virtual void FSMStart() { }
    protected virtual void FSMUpdate() { }

    //private 
    //FindObjectsOfType<Agent>()

    // Use this for initialization
    void Start () {

        if (character_settings.current_character == 0)
        {
            player = GameObject.Find("Yin");

        }
        else
        {
            player = GameObject.Find("Yang");
        }

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = movementSpeed;
        
        FSMStart();
	}
	
	// Update is called once per frame
	void Update () {
        FSMUpdate();

        if (character_settings.current_character == 0)
        {
            player = GameObject.Find("Yin");

        }
        else
        {
            player = GameObject.Find("Yang");
        }
    }
}