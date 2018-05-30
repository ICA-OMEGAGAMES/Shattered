using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkill : MonoBehaviour
{
    public float skillDuration;

    GameObject player;
    bool skillActive;

    float slowDownFactor = 0.03f;

    Vector3 originalPos = new Vector3(0, -10, 0);

    // Use this for initialization
    void Start()
    {
        skillActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(Constants.SKILL3_BUTTON) && !skillActive)
        {
            StartCoroutine(startSkillTimer());
            startSlowMotion();
        }
        else if (skillActive)
        {
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
        stopSlowMotion();
    }

    void doTeleportSkill()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Create Cube at the Teleportation Position
            // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // cube.transform.position = this.transform.position;

            player = GameObject.Find("Dave");
            //should I search other objects to?

            player.transform.position = this.transform.position;
            skillActive = false;
            stopSlowMotion();
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

    void startSlowMotion()
    {
        Time.timeScale = 0.0f;
		// Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    void stopSlowMotion()
    {
        this.transform.position = originalPos;
        Time.timeScale = 1.0F;
        // Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
