using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameUI : MonoBehaviour
{
    public GameObject DailyQuest;
    public GameObject WeeklyQuest;

    public void OpenDaily(bool isDaily)
    {
        if(isDaily)
        {
            DailyQuest.SetActive(true);
            WeeklyQuest.SetActive(false);
        }
        else
        {
            DailyQuest.SetActive(false);
            WeeklyQuest.SetActive(true);
        }
    }
}
