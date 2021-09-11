using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

class TempClass // 충돌 안나게 하려고 만든 클래스! 수정하지말아주세요 히히
{
    public TempClass()
    {
        DDOLObj.Instance.ScoreInit = () =>
        {
            for (int i = 0; i < DDOLObj.Instance.miniGameCount; i++)
            {
                PlayerPrefs.SetInt($"{i}_First", PlayerPrefs.GetInt($"{i}_First"));
                PlayerPrefs.SetInt($"{i}_Second", PlayerPrefs.GetInt($"{i}_Second"));
                PlayerPrefs.SetInt($"{i}_Third", PlayerPrefs.GetInt($"{i}_Third"));
            }
        };
    }
}

public class DDOLObj : MonoBehaviour
{
    public static DDOLObj Instance { get; private set; } = null;
    public int[] ClearList = new int[3];
    public int SelectMiniGame { get; set; }
    public int miniGameCount = 4;
    public UnityEngine.Object tempObj = null;
    DateTime time;

    public delegate void Init();
    public Init ScoreInit = null;

    private void Awake()
    {
        if (time.Year <= 1)
            time = DateTime.Now;

        var obj = FindObjectsOfType<DDOLObj>();
        if (obj.Length == 1)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            TempClass tc = new TempClass();

            ScoreInit();
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

        GameReset();
    }

    void GameReset()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TempClass tc = new TempClass();

            for (int i = 0; i < miniGameCount; i++)
            {
                PlayerPrefs.SetInt($"{i}_First", 0);
                PlayerPrefs.SetInt($"{i}_Second", 0);
                PlayerPrefs.SetInt($"{i}_Third", 0);
            }

            PlayerPrefs.DeleteKey("First");
            PlayerPrefs.DeleteKey("과일 수확");
            PlayerPrefs.DeleteKey("잡초 뽑기");
            PlayerPrefs.DeleteKey("쓰레기 분리수거");
        }
    }

    public void GameClear() // 미니게임 클리어 시 불리는 함수
    {
        if (ClearList[SelectMiniGame] < 1)
            ClearList[SelectMiniGame] = 1;
        SceneManager.LoadScene("Ingame");
    }
}
