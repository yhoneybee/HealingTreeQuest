using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 과일 수확
public class GameSystem3 : MonoBehaviour
{
    public ScoreSystem scoreSystem;
    TimerSystem timerSystem;
    DirectorSystem directorSystem;

    Text scoreText;
    Text timeText;

    public Slider moveBar;

    GameObject fruit;
    void Start()
    {
        scoreSystem = Tools<ScoreSystem>.GetTool("ScoreSystem");
        timerSystem = Tools<TimerSystem>.GetTool("TimerSystem");
        directorSystem = Tools<DirectorSystem>.GetTool("DirectorSystem");
        directorSystem.visualSystem.Tutorial = Tutorial;

        scoreText = Tools<Text>.GetTool("ScoreText");
        timeText = Tools<Text>.GetTool("TimeText");

        moveBar = Tools<Slider>.GetTool("MoveBar");

        fruit = Resources.Load<GameObject>("Prefabs/MiniGame/과일 수확/Fruit");

        timerSystem.TimerStart(60);

        StartCoroutine(RandomSpawn());
    }

    void Update()
    {
        timerSystem.SetTimeText(ref timeText);
        scoreSystem.SetScoreText(ref scoreText);
    }
    void Tutorial()
    {

    }
    IEnumerator RandomSpawn()
    {
        float spawnTime = 2f;
        while (true)
        {
            if (timerSystem.timeUp)
            {
                int[] scoreChart = scoreSystem.GetScoreChart(2);
                directorSystem.visualSystem.ResultAnimation(scoreSystem.GetScore(), scoreChart);
                yield break;
            }

            else if (timerSystem.GetTime() <= 20)
                spawnTime = 0.1f;
            else if (timerSystem.GetTime() <= 40)
                spawnTime = 0.5f;

            yield return new WaitForSeconds(spawnTime);
            GameObject obj = Instantiate(fruit, new Vector2(Random.Range(-2.2f, 2.2f), 4), Quaternion.identity);
            obj.transform.localScale = new Vector2(0.25f, 0.25f);
            obj.GetComponent<Rigidbody2D>().gravityScale = 0;
            while (true)
            {
                obj.transform.localScale = Vector2.Lerp(obj.transform.localScale, new Vector2(0.5f, 0.5f), 0.2f);
                yield return new WaitForSeconds(0.01f);
                if (obj.transform.localScale.x >= 0.49f)
                {
                    obj.transform.localScale = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<Rigidbody2D>().gravityScale = 1;
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            scoreSystem.ScoreMinus(50);
            Destroy(collision.gameObject);
        }
    }
}
