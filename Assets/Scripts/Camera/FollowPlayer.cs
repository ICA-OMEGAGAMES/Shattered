using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject gameObjectToFollow;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update () {
        CheckActiveFollowPoint();
        Follow();
    }
    void Follow() {
        transform.position = new Vector3 (gameObjectToFollow.transform.position.x, 0, gameObjectToFollow.transform.position.z);
    }
    private void CheckActiveFollowPoint() {
        if (!gameObjectToFollow.activeInHierarchy) {
            gameObjectToFollow = GameObject.Find(gameObjectToFollow.name);
        }
    }
}
