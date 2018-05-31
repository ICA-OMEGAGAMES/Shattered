using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Yarn.Unity.Shattered {
    public class DialoguePlayer : MonoBehaviour {

        public float interactionRadius = 2.0f;

        public float MovementFromButtons {get;set;}
        
        void Update () {

            // Remove all player control when we're in dialogue
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true) {
                return;
            }

            // Detect if we want to start a conversation
            if (Input.GetKeyDown(KeyCode.Space)) {
                CheckForNearbyNPC ();
            }

            // Movement for Testing
            // transform.Translate(1f * Input.GetAxisRaw("Horizontal") + Time.deltaTime, 0f, 1f * Input.GetAxisRaw("Vertical") * Time.deltaTime);
        }

        /// Find all DialogueParticipants
        /** Filter them to those that have a Yarn start node and are in range; 
         * then start a conversation with the first one
         */
        public void CheckForNearbyNPC ()
        {
            var allParticipants = new List<DialogueNPC> (FindObjectsOfType<DialogueNPC> ());
            var target = allParticipants.Find (delegate (DialogueNPC p) {
                return string.IsNullOrEmpty (p.talkToNode) == false && // has a conversation node?
                (p.transform.position - this.transform.position)// is in range?
                .magnitude <= interactionRadius;
            });
            if (target != null) {
                // Kick off the dialogue at this node.
                FindObjectOfType<DialogueRunner> ().StartDialogue (target.talkToNode);
            }
        }
    }
}
