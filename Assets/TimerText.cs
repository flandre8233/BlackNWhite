using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerText : SingletonMonoBehavior<TimerText>
{
    TimeSpan timeSpan;
    Time time;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
         text = GetComponent<Text>();

        globalUpdateManager.instance.registerUpdateDg(ToUpdate);
    }

    // Update is called once per frame
    public void ToUpdate()
    {
        timeSpan = TimeSpan.FromSeconds(manager.instance.Timer);
        string TimeString = string.Format("{0}:{01:00}",
(int)timeSpan.TotalMinutes, // <== Note the casting to int.
        timeSpan.Seconds);

        text.text =  "Time : " + TimeString;
    }
}
