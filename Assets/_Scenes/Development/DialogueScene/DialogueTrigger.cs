using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using Yarn.Unity.Shattered;

public class DialogueTrigger : MonoBehaviour
{
	public string yarnFileName;
    bool dialogueIsDone = false;

    [Header("Optional")]
    public TextAsset scriptToLoad;

    void Start()
    {
		this.GetComponent<MeshRenderer>().enabled = false;

        if (scriptToLoad != null)
        {
            FindObjectOfType<Yarn.Unity.Shattered.DialogueRunner>().AddScript(scriptToLoad);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dialogueIsDone)
        {
            //start and activate the dialogue
            Debug.Log("DialogueShouldStart");
            dialogueIsDone = true;

			//Freeze the game?! or freeze just player movement? other wise the dialogue frezzes to
			// Time.timeScale = 0.0f;

			//Activate the DialogueCanvas..
            FindObjectOfType<DialogueRunner>().dialogueCanvas.SetActive(true);

			// Kick off the dialogue at this node.
            FindObjectOfType<DialogueRunner>().StartDialogue(yarnFileName);
        }
        else
        {
            //DIalogue already played, do nothing..
            Debug.Log("DialogueIsDone");
        }
    }
}
