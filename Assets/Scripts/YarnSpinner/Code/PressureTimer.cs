using UnityEngine;
using System.Collections;
using System.Timers;

public class PressureTimer
{
    private static PressureTimer instance;
    public int time { get; set; }
    public int selectedOption { get; set; }
    public bool isActive { get; set; }

    private PressureTimer() { }

    public static PressureTimer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PressureTimer();
            }
            return instance;
        }
    }
}