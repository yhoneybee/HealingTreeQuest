using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem3 : MonoBehaviour
{
    ScoreSystem scoreSystem;
    TimerSystem timerSystem;

    Text scoreText;
    Text timeText;
    void Start()
    {
        scoreSystem = Tools<ScoreSystem>.GetTool("ScoreSystem");
        timerSystem = Tools<TimerSystem>.GetTool("TimerSystem");

        scoreText = Tools<Text>.GetTool("ScoreText");
        timeText = Tools<Text>.GetTool("TimeText");

        timerSystem.TimerStart(60);
    }

    void Update()
    {
        timerSystem.SetTimeText(ref timeText);
    }
}
