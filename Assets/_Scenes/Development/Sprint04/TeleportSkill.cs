using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkill : MonoBehaviour
{
    // public bool isActive;
    public TimeManager timeManager;

    // Use this for initialization
    void Start()
    {
        // isActive = false;
        // this.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(Constants.SKILL3_BUTTON))
        {
            Debug.Log("StartSkill");
            timeManager.DoSlowmotion();
            startTeleportSkill();
        }
    }

    void startTeleportSkill()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = this.transform.position;
        }

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            if (hit.transform.CompareTag("Floor"))
            {
                this.transform.position = hit.point;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z);
            }
        }
    }
}
