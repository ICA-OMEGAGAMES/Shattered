using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerPickupActionController : MonoBehaviour
{
    public InventoryObject inventory;
    public SystemObject character_settings;
    public Text pickup_text;

    private GameObject[] level_pickups;
    private GameObject pickup_item;
    private Transform player;

    // Use this for initialization
    void Start()
    {
        level_pickups = GameObject.FindGameObjectsWithTag("Pickup");
    }

    // Update is called once per frame
    void Update()
    {
        if (character_settings.current_character == 0)
        {
            player = GameObject.Find("Yin").transform;
        }
        else
        {
            player = GameObject.Find("Yang").transform;
        }

        if (Input.GetAxisRaw("Pickup") > 0)
        {
            pickup_item = getClosestItem();
            addToInventory();
            
            //REMOVE AFTER DEMO
            if (pickup_item.name == "Key")
            {
                inventory.has_key = true;
            }

            pickup_item.SetActive(false);
            resetMessage();
            

        }
    }

    void addToInventory()
    {
        //REMOVE AFTER DEMO
        //inventory.pickups[0] = pickup_item.gameObject;
        
        //FIX AFTER DEMO
        //for (int i=0; i < inventory.pickups.Length; i++)
        //{
        //    if (inventory.pickups[i] != null)
        //    {
        //        inventory.pickups[i] = pickup_item;
        //    }
        //    else
        //    {
        //        //implement a message popup here for if the inventory is full
        //        break;
        //    }
        //}
    }

    GameObject getClosestItem()
    {
        GameObject closest_item = level_pickups[0];
        Vector3 closest_item_vector = closest_item.transform.position;

        //skip the first element as we're setting closest_item to be the first object in the array
        for (int i=1; i < level_pickups.Length; i++)
        {
            if (Vector3.Distance(level_pickups[i].transform.position, player.position) < Vector3.Distance(closest_item_vector, player.position))
            {
                closest_item = level_pickups[i];
                closest_item_vector = closest_item.transform.position;
            }
        }

        return closest_item;
    }

    private void resetMessage()
    {
        pickup_text.text = "";
    }
}
