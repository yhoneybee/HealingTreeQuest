using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem1 : MonoBehaviour
{
    List<Weeds> weeds = new List<Weeds>();
    Text scoreText;
    Text timeText;
    Sprite[] sprites;
    GameObject[] spawnPoints;
    ScoreSystem scoreSystem;
    TimerSystem timeSystem;

    float spawnTime = 1f;
    bool gameFinish = false;

    void Start()
    {
        scoreSystem = Tools<ScoreSystem>.GetTool("ScoreSystem");
        timeSystem = Tools<TimerSystem>.GetTool("TimeSystem");

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        sprites = Resources.LoadAll<Sprite>("Sprites/MiniGame/잡초 뽑기/잡초");

        scoreText = Tools<Text>.GetTool("ScoreText");
        timeText = Tools<Text>.GetTool("TimeText");

        timeSystem.TimerStart(60);

        StartCoroutine(RandomSpawn());
    }

    void Update()
    {
        for (int i = 0; i < weeds.Count; i++)
        {
            if (weeds[i].slider.value >= 4f)
            {
                weeds[i].Release();
                weeds.Remove(weeds[i]);

                scoreSystem.ScoreMinus(50);
                scoreText.text = scoreSystem.GetScore().ToString();
                continue;
            }
            weeds[i].GetComponent<Image>().sprite = sprites[(int)weeds[i].slider.value];
        }

        timeSystem.SetTimeText(ref timeText);
        if (timeSystem.GetTime() <= 0)
        {
            gameFinish = true;
            timeSystem.CancelInvoke("CountDown");
            StopCoroutine(RandomSpawn());
        }
        else if (timeSystem.GetTime() <= 20)
        {
            spawnTime = 0.2f;
        }
        else if (timeSystem.GetTime() <= 40)
        {
            spawnTime = 0.5f;
        }
    }

    IEnumerator RandomSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            int random = 0;
            Image image = null;

            int count = 0;
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i].GetComponent<Image>().enabled)
                    count++;
            }

            do
            {
                if (count >= spawnPoints.Length) break;
                random = Random.Range(0, spawnPoints.Length);
                image = spawnPoints[random].GetComponent<Image>();
            } while (image.enabled);

            if (!(count >= spawnPoints.Length))
            {
                Weeds weed = spawnPoints[random].GetComponent<Weeds>();
                weed.Init(spawnPoints[random]);
                image.sprite = sprites[0];
                weeds.Add(weed);
            }
        }
    }

    public void ClickWeeds(Weeds weeds)
    {
        Image image = weeds.GetComponent<Image>();
        if (!image.sprite) return;

        weeds.Release();

        scoreSystem.ScorePlus(100);
        scoreText.text = scoreSystem.GetScore().ToString();
    }
}
