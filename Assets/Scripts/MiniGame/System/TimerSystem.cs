using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSystem : MonoBehaviour
{
    [SerializeField] Text timeText;

    [HideInInspector]
    public bool timeUp = false;
    private int time;

    public void TimerStart(int time)
    {
        this.time = time;
        InvokeRepeating("CountDown", 1, 1);
    }

    private void CountDown()
    {
        if (time <= 0)
        {
            timeUp = true;
            CancelInvoke("CountDown");
            return;
        }

        time--;
        SetTimeText();
    }

    public int GetTime()
    {
        return time;
    }

    public void SetTimeText()
    {
        timeText.text = GetTimeText();
    }

    private string GetTimeText()
    {
        string timeText;
        timeText = time.ToString();
        return timeText;
    }
}
