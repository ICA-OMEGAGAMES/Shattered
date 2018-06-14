using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

namespace Yarn.Unity.Shattered
{

    public class YarnCommands : SceneTransition
    {

        private IEnumerator coroutine;
        public float durationForStartingCutscene = 35.0f;
        public bool isHomelessDialogueFinished;
        public GameObject malphasCutsceneImage;
        public AudioClip malphasCutsceneAudio;
        private float combatTimestamp;
        private int currentScene;

        void Start()
        {
            coroutine = WaitForUnitlSoundIsFinished();
            currentScene = SceneManager.GetActiveScene().buildIndex;
        }

        [YarnCommand("doJump")]
        public void jump(string jumpheight)
        {
            // simulate the Jump function
            Vector3 originalPos = transform.position;
            transform.position = new Vector3(originalPos.x, originalPos.y, originalPos.z + 1.0f);
        }

        [YarnCommand("startPressureTimer")]
        public void pressureTimer(string parameters)
        {
            //We get all parameters and split them, so that we can use these later
            var para = parameters.Split(',');

            int seconds = int.Parse(para[0]);
            int definedOption = int.Parse(para[1]) - 1;

            // set the values, which we get from the command and put it into the PressureTimer
            PressureTimer.Instance.isActive = true;
            PressureTimer.Instance.time = seconds;

            if (definedOption < 0)
            {
                definedOption = 0;
            }

            PressureTimer.Instance.selectedOption = definedOption;
        }

        [YarnCommand("startHelpTimer")]
        public void helpTimer(string parameters)
        {
            var para = parameters.Split(',');

            //We get all parameters and split them, so that we can use these later
            int seconds = int.Parse(para[0]);
            int optionForHelping = int.Parse(para[1]) - 1;

            // set the values, which we get from the command and put it into the HelpTimer
            HelpTimer.Instance.isActive = true;
            HelpTimer.Instance.time = seconds;

            // Check if optionForHelping is lower than 0
            if (optionForHelping < 0)
            {
                optionForHelping = 0;
            }

            HelpTimer.Instance.optionForHelping = optionForHelping;
        }

        [YarnCommand("openSkillTree")]
        public void openSkillTree(string parameters)
        {
            FindObjectOfType<SkillTreeMenu>().openWithinDialogueSystem();
        }

        [YarnCommand("startMalphasCutscene")]
        public void StartMalphasCutscene(string voiceName)
        {
            //Start AI
            isHomelessDialogueFinished = true;

            
            //Freeze game when cutscene shows off
            // Time.timeScale = 0.0f;

            StartCoroutine(WaitForUnitlSoundIsFinished());
        }

        IEnumerator WaitForUnitlSoundIsFinished()
        {
            yield return StartCoroutine(WaitForCutscene());

            //Set Image visible
            malphasCutsceneImage.SetActive(true);

            //Play Audio
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = malphasCutsceneAudio;
            audio.Play();
            float duration = malphasCutsceneAudio.length;

            yield return StartCoroutine(WaitForAudio(duration));

            //Unfreeze Game
            // Time.timeScale = 1.0f;

            //After audio disable Image
            // malphasCutsceneImage.SetActive(false);

            //From here on start the Dialogue with Malphas!!
            startMalphasDialogue();
        }

        IEnumerator WaitForCutscene()
        {
            combatTimestamp = Time.time + durationForStartingCutscene;
            while(Time.time < combatTimestamp)
            {
                if(GameObject.FindObjectOfType<Statistics>().health <= 0)
                {
                    yield return new WaitForSeconds(2.5f);
                    break;
                }
                yield return null;
            }
        }

        IEnumerator WaitForAudio(float duration)
        {
            yield return new WaitForSeconds(duration);
        }

        void startMalphasDialogue()
        {
            Debug.LogError("StartMalphasDialogue");
            //Activate the DialogueCanvas..
            FindObjectOfType<DialogueRunner>().dialogueCanvas.SetActive(true);

			// Kick off the dialogue at this node.
            FindObjectOfType<DialogueRunner>().StartDialogue("Phase6_Temptation");
        }

        [YarnCommand("changeToEndgame")]
        public void ChangeToEndgame(string parameters)
        {
            Debug.Log("start yarn transition");  

            StartCoroutine(LoadingScreen());
        }
    }
}