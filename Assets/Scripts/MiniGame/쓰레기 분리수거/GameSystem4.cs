using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 쓰레기 분리수거
public class GameSystem4 : MonoBehaviour
{
    [HideInInspector]
    public ScoreSystem scoreSystem { get; set; }
    TimerSystem timerSystem;
    public DirectorSystem directorSystem { get; set; }
    public UISystem uiSystem { get; set; }

    public Transform[] spawnPoints;

    [SerializeField] GameObject fadeA;
    [SerializeField] GameObject fadeB;

    Trash[] trashs;

    Text targetScore;

    int clearScore = 6000;
    void Start()
    {
        scoreSystem = Tools<ScoreSystem>.GetTool("ScoreSystem");
        timerSystem = Tools<TimerSystem>.GetTool("TimerSystem");
        directorSystem = Tools<DirectorSystem>.GetTool("DirectorSystem");
        uiSystem = Tools<UISystem>.GetTool("UISystem");

        targetScore = Tools<Text>.GetTool("TargetScore");
        targetScore.text = clearScore.ToString();

        trashs = Resources.LoadAll<Trash>("Prefabs/MiniGame/쓰레기 분리수거");

        directorSystem.visualSystem.TutorialOfIndex = new VisualSystem.Tutorials[directorSystem.visualSystem.tutorialTexts.Length];
        directorSystem.visualSystem.TutorialOfIndex[2] = () => { fadeA.SetActive(true); directorSystem.visualSystem.FadeIn(fadeA, 0.5f); };
        directorSystem.visualSystem.TutorialOfIndex[4] = () => { fadeA.SetActive(false); fadeB.SetActive(true); directorSystem.visualSystem.FadeIn(fadeB, 0.5f); };
        directorSystem.visualSystem.TutorialOfIndex[7] = () => { fadeB.SetActive(false); directorSystem.visualSystem.FadeIn(fadeB, 0.5f); };
        directorSystem.visualSystem.AfterTutorial = () => { timerSystem.TimerStart(60); StartCoroutine(Spawn()); };
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
                    Trash trash = Instantiate(trashs[Random.Range(0, trashs.Length)], spawnPoints[randomValue].transform);
                    trash.destroyTime = destroyTime;
                }
            }

            if (timerSystem.timeUp)
            {
                bool gameClear = scoreSystem.GetScore() >= clearScore;

                int[] scoreChart = scoreSystem.GetScoreChart(3);
                directorSystem.isGameEnd = true;
                directorSystem.visualSystem.ResultAnimation(scoreSystem.GetScore(), scoreChart, gameClear);
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
