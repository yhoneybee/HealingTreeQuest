using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem2 : MonoBehaviour
{
    ScoreSystem scoreSystem;
    TimerSystem timerSystem;
    void Start()
    {
        scoreSystem = Tools<ScoreSystem>.GetTool("ScoreSystem");
        timerSystem = Tools<TimerSystem>.GetTool("TimerSystem");
    }

    void Update()
    {
        
    }
}
