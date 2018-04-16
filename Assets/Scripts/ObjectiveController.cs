using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController : MonoBehaviour {

    public ObjectiveScriptableObject objective_obj;
    public InventoryObject inv_obj;
    public SystemObject character_settings;
    public GameObject victory_toilet;
    public Text obj_text;

    private Transform player_transform;
    private string[] objectives;
    // Use this for initialization
    void Start() {
        objectives = objective_obj.objectives;
        obj_text.text = objectives[0];
        
    }
	
	// Update is called once per frame
	void Update () {
        if (character_settings.current_character == 0)
        {
            player_transform = GameObject.Find("Yin").transform;
        }
        else
        {
            player_transform = GameObject.Find("Yang").transform;
        }

        if (inv_obj.has_key == true)
        {
            obj_text.text = objectives[1];
            if (Vector3.Distance(victory_toilet.transform.position, player_transform.position) < 1f)
            {
                obj_text.text = objectives[2];
            }
        }	
	}
}
