﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Yarn.Unity.Shattered
{
    public class DialoguePlayer : MonoBehaviour
    {
        public float interactionRadius = 2.0f;

        public float MovementFromButtons { get; set; }

        void Update()
        {
            // Remove all player control when we're in dialogue
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
            {
                return;
            }

            // Detect if we want to start a conversation
            // if (Input.GetKeyDown(KeyCode.Space)) {
            if (Input.GetMouseButtonDown(0))
            {
                CheckForNearbyNPC();
            }
        }

        /// Find all DialogueParticipants
        /** Filter them to those that have a Yarn start node and are in range; 
         * then start a conversation with the first one
         */
        public void CheckForNearbyNPC()
        {
            var allParticipants = new List<DialogueNPC>(FindObjectsOfType<DialogueNPC>());
            var target = allParticipants.Find(delegate (DialogueNPC p)
            {
                return string.IsNullOrEmpty(p.talkToNode) == false && // has a conversation node?
                (p.transform.position - this.transform.position)// is in range?
                .magnitude <= interactionRadius;
            });
            if (target != null)
            {
                //Activate the DialogueCanvas..
                FindObjectOfType<DialogueRunner>().dialogueCanvas.SetActive(true);

                // Kick off the dialogue at this node.
                FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
            }
        }
    }
}
