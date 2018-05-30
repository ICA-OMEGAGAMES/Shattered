using UnityEngine;
using System.Collections;

namespace Yarn.Unity.Shattered
{

    public class YarnCommands : MonoBehaviour
    {

        [YarnCommand("doJump")]
        public void jump(string jumpheight)
        {
            Debug.Log("Command --> doJump");

            // simulate the Jump function
            Vector3 originalPos = transform.position;
            transform.position = new Vector3(originalPos.x, originalPos.y, originalPos.z + 1.0f);
        }

        [YarnCommand("startPressureTimer")]
        public void pressureTimer(string parameters)
        {
            Debug.Log("Command --> startPressureTimer");

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
            Debug.Log("Command --> startHelpTimer");
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
            Debug.Log("Command --> openSkillTree");

            FindObjectOfType<SkillTreeMenu>().openWithinDialogueSystem();
        }
    }
}