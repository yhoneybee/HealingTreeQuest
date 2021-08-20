using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem4 : MonoBehaviour
{
    [HideInInspector]
    public ScoreSystem scoreSystem;
    TimerSystem timerSystem;
    DirectorSystem directorSystem;

    public Transform[] spawnPoints;
    Text scoreText;
    Text timeText;
    Button OKButton;
    Trash trash;
    void Start()
    {
        scoreSystem = Tools<ScoreSystem>.GetTool("ScoreSystem");
        timerSystem = Tools<TimerSystem>.GetTool("TimerSystem");
        directorSystem = Tools<DirectorSystem>.GetTool("DirectorSystem");

        scoreText = Tools<Text>.GetTool("ScoreText");
        timeText = Tools<Text>.GetTool("TimeText");
        OKButton = Tools<Button>.GetTool("OKButton");
        OKButton.onClick.AddListener(()=> { DDOLObj.Instance.GameClear(); });

        trash = Resources.Load<Trash>("Prefabs/MiniGame/쓰레기 분리수거/Trash");

        timerSystem.TimerStart(60);

        StartCoroutine(Spawn());
    }

    void Update()
    {
        scoreSystem.SetScoreText(ref scoreText);
        timerSystem.SetTimeText(ref timeText);
    }

    IEnumerator Spawn()
    {
        int spawnCount = 1;
        float spawnDelay = 2f;
        float destroyTime = 5f;

        int randomValue;

        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                if (spawnPoints[0].childCount + spawnPoints[1].childCount + spawnPoints[2].childCount < spawnCount)
                {
                    do
                    {
                        randomValue = Random.Range(0, spawnPoints.Length);
                    } while (spawnPoints[randomValue].childCount != 0);
                    Trash trash = Instantiate(this.trash, spawnPoints[randomValue].transform);
                    trash.destroyTime = destroyTime;
                }
            }

            if (timerSystem.timeUp)
            {
                directorSystem.visualSystem.ResultAnimation();
                yield break;
            }
            else if (timerSystem.GetTime() <= 20)
            {
                spawnCount = 3;
                spawnDelay = 0.5f;
                destroyTime = 3f;
            }
            else if (timerSystem.GetTime() <= 40)
            {
                spawnCount = 2;
                spawnDelay = 1f;
                destroyTime = 4f;
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
