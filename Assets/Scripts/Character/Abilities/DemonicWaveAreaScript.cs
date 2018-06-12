using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicWaveAreaScript : MonoBehaviour {

    public SkillSettings skillSettings;

    private List<GameObject> hitTargets = new List<GameObject>();

	void Start () {
        Destroy(this.gameObject, skillSettings.duration);
        if(GameObject.Find("Spikes"))
            GameObject.Find("Spikes").GetComponent<ParticleSystem>().Play();
    }

    void OnTriggerStay(Collider other)
    {
        //if the hitTarget is not the ai return
        if (other.transform.tag != Constants.ENEMY_TAG)
            return;
        if (!hitTargets.Contains(other.gameObject)) {
             hitTargets.Add(other.gameObject);
            other.transform.GetComponent<AIManager>().TakeDamage(skillSettings.value, Constants.SPECIAL_ABILITY_ATTACK);
        }
    }
}
