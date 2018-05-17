using UnityEngine;
using System.Collections;
using System.Timers;

public class HelpTimer
{
    private static HelpTimer instance;
    public int time { get; set; }
    public int optionForHelping { get; set; }
    public bool isActive { get; set; }

    private HelpTimer() { }

    public static HelpTimer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HelpTimer();
            }
            return instance;
        }
    }
}