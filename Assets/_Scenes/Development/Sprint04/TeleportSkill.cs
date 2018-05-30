using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkill : MonoBehaviour
{
    // public bool isActive;
    public TimeManager timeManager;
    public float skillDuration;

    GameObject player;
    bool skillActive;

    // Use this for initialization
    void Start()
    {
        skillActive = false;
        // this.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(Constants.SKILL3_BUTTON) && !skillActive)
        {
            Debug.Log("StartSkill");
            StartCoroutine(startSkillTimer());
        }
        else if (skillActive)
        {
            timeManager.DoSlowmotion(skillDuration + 5.0f);
            doTeleportSkill();
        }
    }

    IEnumerator startSkillTimer()
    {
        Debug.Log("StartCouroutine");
        skillActive = true;

        yield return new WaitForSeconds(skillDuration);  // Wait three seconds

        Debug.Log("Couroutine Finished");
        skillActive = false;
    }

    void doTeleportSkill()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Create Cube at the Teleportation Position
            // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // cube.transform.position = this.transform.position;

            player = GameObject.Find("Mannequin");
            //should I search other objects to?

            player.transform.position = this.transform.position;
            skillActive = false;
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
