using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour {

    //Will be used when animation is used.
    public Animator animator;
    public GameObject player;
    public int distance;
    private Vector3 current_position;
    private bool is_rolling;

    // Use this for initialization
    void Start() {

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        current_position = player.transform.position;
        is_rolling = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("Dodge") == 1 && is_rolling == false)
        {
            StartCoroutine(Roll());
        }
    }

    IEnumerator Roll(){
        current_position.z += distance;
        player.transform.Translate(current_position * Time.deltaTime);
        is_rolling = true;
        yield return new WaitForSeconds(1f);
        StopCoroutine(Roll());
        is_rolling = false;
    }
}
