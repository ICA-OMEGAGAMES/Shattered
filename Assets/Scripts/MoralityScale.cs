using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MoralityScale : MonoBehaviour
{
    public SystemObject character_settings;
    public Text morality_text;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (character_settings.current_character == 1)
        {
            character_settings.morality_value -= 1;
        }
        else
        {
            character_settings.morality_value += 1;
        }

        morality_text.text = "Morality Value: " + character_settings.morality_value;
    }
}
