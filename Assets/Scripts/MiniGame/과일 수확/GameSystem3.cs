using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem3 : MonoBehaviour
{
    public ScoreSystem scoreSystem;
    TimerSystem timerSystem;
    DirectorSystem directorSystem;

    Text scoreText;
    Text timeText;
    Button OKButton;

    public Slider moveBar;

    GameObject fruit;
    void Start()
    {
        scoreSystem = Tools<ScoreSystem>.GetTool("ScoreSystem");
        timerSystem = Tools<TimerSystem>.GetTool("TimerSystem");
        directorSystem = Tools<DirectorSystem>.GetTool("DirectorSystem");

        scoreText = Tools<Text>.GetTool("ScoreText");
        timeText = Tools<Text>.GetTool("TimeText");
        OKButton = Tools<Button>.GetTool("OKButton");
        OKButton.onClick.AddListener(() => { DDOLObj.Instance.GameClear(); });

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

    IEnumerator RandomSpawn()
    {
        float spawnTime = 2f;
        while (true)
        {
            if (timerSystem.timeUp)
            {
                directorSystem.visualSystem.ResultAnimation();
                yield break;
            }

            else if (timerSystem.GetTime() <= 20)
                spawnTime = 0.1f;
            else if (timerSystem.GetTime() <= 40)
                spawnTime = 0.5f;

            yield return new WaitForSeconds(spawnTime);
            Instantiate(fruit, new Vector2(Random.Range(-2.2f, 2.2f), 4), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            scoreSystem.ScoreMinus(50);
            Destroy(collision.gameObject);
            directorSystem.cameraSystem.ShakeCam(1, 0.1f);
        }
    }
}
