using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

// ���� ���潺
public class GameSystem2 : MonoBehaviour
{
    public ScoreSystem scoreSystem;
    TimerSystem timerSystem;

    public EnemyPool enemyPool;

    public Text scoreText;
    public Text timeText;

    Transform[] spawnPoints = new Transform[4];

    Defencer defencer;

    Vector2[] mousePos = new Vector2[2];
    void Start()
    {
        scoreSystem = Tools<ScoreSystem>.GetTool("ScoreSystem");
        timerSystem = Tools<TimerSystem>.GetTool("TimerSystem");

        enemyPool = Tools<EnemyPool>.GetTool("EnemyPool");

        scoreText = Tools<Text>.GetTool("ScoreText");
        timeText = Tools<Text>.GetTool("TimeText");

        defencer = Tools<Defencer>.GetTool("Defencer");

        GameObject PointsParent = GameObject.Find("SpawnPoints");
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = PointsParent.transform.GetChild(i);
        }

        timerSystem.TimerStart(60);

        StartCoroutine(RandomSpawn());
    }

    void Update()
    {
        DragScreen();

        timerSystem.SetTimeText(ref timeText);
    }

    void DragScreen()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos[0] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mousePos[1] = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            SetDefencerDir();
        }
    }

    void SetDefencerDir()
    {
        float xDistance = Vector2.Distance(new Vector2(mousePos[0].x, 0), new Vector2(mousePos[1].x, 0));
        float yDistance = Vector2.Distance(new Vector2(0, mousePos[0].y), new Vector2(0, mousePos[1].y));

        if (xDistance >= 1f && xDistance > yDistance)
        {
            defencer.SetDir((mousePos[0].x > mousePos[1].x) ?
                Direction.Left :
                Direction.Right);
        }
        else if (yDistance >= 1f && yDistance > xDistance)
        {
            defencer.SetDir((mousePos[0].y > mousePos[1].y) ?
                Direction.Down :
                Direction.Up);
        }
    }

    IEnumerator RandomSpawn()
    {
        float spawnTime = 2f;
        while (true)
        {
            if (timerSystem.timeUp) yield break;
            else if (timerSystem.GetTime() <= 40)
            {
                spawnTime = 1f;
            }
            else if (timerSystem.GetTime() <= 20)
            {
                spawnTime = 0.5f;
            }
            yield return new WaitForSeconds(spawnTime);

            int random = Random.Range(0, 4);

            Enemy enemy = enemyPool.GetEnemy();
            enemy.transform.position = spawnPoints[random].position;

            switch (random)
            {
                case 0:
                    enemy.SetDirection(Direction.Left);
                    break;
                case 1:
                    enemy.SetDirection(Direction.Right);
                    break;
                case 2:
                    enemy.SetDirection(Direction.Up);
                    break;
                case 3:
                    enemy.SetDirection(Direction.Down);
                    break;
            }
        }
    }
}
