using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
/// attached to the non-player characters, and stores the name of the
/// Yarn node that should be run when you talk to them.
namespace Yarn.Unity.Shattered
{
    public class DialogueNPC : MonoBehaviour
    {
        public string characterName = "";

        [FormerlySerializedAs("startNode")]
        public string talkToNode = "";

        [Header("Optional")]
        public TextAsset scriptToLoad;
        
        void Start()
        {
            if (scriptToLoad != null)
            {
                FindObjectOfType<Yarn.Unity.Shattered.DialogueRunner>().AddScript(scriptToLoad);
            }

        }
    }

}
