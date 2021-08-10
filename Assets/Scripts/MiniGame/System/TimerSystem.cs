using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSystem : MonoBehaviour
{
    private int time;

    public void TimerStart(int time)
    {
        this.time = time;
        InvokeRepeating("CountDown", 1, 1);
    }

    private void CountDown()
    {
        time--;
    }

    public int GetTime()
    {
        return time;
    }

    public void SetTimeText(ref Text timeText)
    {
        timeText.text = GetTimeText();
    }

    private string GetTimeText()
    {
        string timeText;
        timeText = $"{time / 60} : {time % 60}";
        return timeText;
    }
}
