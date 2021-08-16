using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DonDestroyOnLoadObj : MonoBehaviour
{
    public static DonDestroyOnLoadObj instance;
    public int[] ClearList = new int[3];
    public int SelectMiniGame;
    DateTime time;

    private void Awake()
    {
        if (time.Year <= 1)
            time = DateTime.Now;

        var obj = FindObjectsOfType<DonDestroyOnLoadObj>();
        if (obj.Length == 1)
        {
            instance = this;
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
        if (Input.GetKeyDown(KeyCode.Space))    // Ŭ���� ����
        {
            if (ClearList[SelectMiniGame] < 1)
                ClearList[SelectMiniGame] = 1;
            SceneManager.LoadScene("Ingame");
        }

        if (time.Day != DateTime.Now.Day)   // ���� �ٲ�� ���� �ʱ�ȭ
        {
            time = DateTime.Now;

            for (int i = 0; i < 3; i++)
            {
                ClearList[i] = 0;
            }

            if (SceneManager.GetActiveScene().name == "Ingame") //�ΰ����̸� ����Ʈâ�� �ʱ�ȭ
            {
                var obj = GameObject.FindGameObjectsWithTag("Daily");

                foreach (var item in obj)
                {
                    item.GetComponent<MiniGameProp>().ClearNumber = 0;
                }
            }
        }

    }
}
