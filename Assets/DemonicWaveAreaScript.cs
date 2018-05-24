using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicWaveAreaScript : MonoBehaviour {

    public SkillSettings skillSettings;

    private List<GameObject> hitTargets = new List<GameObject>();

	void Start () {
        Destroy(this.gameObject, skillSettings.duration);
	}

    void OnTriggerStay(Collider other)
    {
        //if the hitTarget is not the ai return
        if (other.transform.tag != Constants.ENEMY_TAG)
            return;
        if (!hitTargets.Contains(other.gameObject)) {
           //todo: after maarten's info add damage system (dot/instant)
             hitTargets.Add(other.gameObject);

            Component component = other.transform.root.GetComponentInChildren<AIManager>();
            if (component != null)
                ((AIManager)component).TakeDamage(skillSettings.value);
        }
    }
}
