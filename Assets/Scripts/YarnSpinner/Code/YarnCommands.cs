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
            transform.position = new Vector3(originalPos.x, originalPos.y + 1.0f, originalPos.z);
        }

        [YarnCommand("startPressureTimer")]
        public void pressureTimer(string test)
        {
            Debug.Log("Command --> startPressureTimer");

            //We get all parameters and split them, so that we can use these later
            var parameters = test.Split(',');
            
            // float seconds = (float) double.Parse(parameters[0]);
            int definedOption = int.Parse(parameters[1]) - 1;
            int seconds = int.Parse(parameters[0]);

            // set the values, which we get from the command and put it into the PressureTimer
            PressureTimer.Instance.isActive = true;
            PressureTimer.Instance.time = seconds;

            // Check if definedOption is lower than 0
            if (definedOption < 0)
            {
                definedOption = 0;
            }

            PressureTimer.Instance.selectedOption = definedOption;
        }

        [YarnCommand("startHelpTimer")]
        public void helpTimer(string time) {
            Debug.Log("Command --> startHelpTimer");
        }
    }
}