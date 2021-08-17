using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DDOLObj : MonoBehaviour
{
    public static DDOLObj Instance { get; private set; } = null;
    public int[] ClearList = new int[3];
    public int SelectMiniGame;
    DateTime time;

    private void Awake()
    {
        if (time.Year <= 1)
            time = DateTime.Now;

        var obj = FindObjectsOfType<DDOLObj>();
        if (obj.Length == 1)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {

    }

    void Update()
    {
        if (time.Day != DateTime.Now.Day)   // 날이 바뀌면 일퀘 초기화
        {
            time = DateTime.Now;

            for (int i = 0; i < 3; i++)
            {
                ClearList[i] = 0;
            }

            if (SceneManager.GetActiveScene().name == "Ingame") //인게임이면 퀘스트창도 초기화
            {
                var obj = GameObject.FindGameObjectsWithTag("Daily");

                foreach (var item in obj)
                {
                    item.GetComponent<MiniGameProp>().ClearNumber = 0;
                }
            }
        }
    }

    public void GameClear() // 미니게임 클리어 시 불리는 함수
    {
        if (ClearList[SelectMiniGame] < 1)
            ClearList[SelectMiniGame] = 1;
        SceneManager.LoadScene("Ingame");
    }
}
