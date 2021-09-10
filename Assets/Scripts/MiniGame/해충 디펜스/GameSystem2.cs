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

// ÇØÃæ µðÆæ½º
public class GameSystem2 : MonoBehaviour
{
    [HideInInspector]
    public ScoreSystem scoreSystem;
    [HideInInspector]
    public DirectorSystem directorSystem;
    UISystem uiSystem;
    TimerSystem timerSystem;

    public EnemyPool enemyPool;

    Transform[] spawnPoints = new Transform[4];

    Defencer defencer;

    Vector2[] mousePos = new Vector2[2];
    void Start()
    {
        scoreSystem = Tools<ScoreSystem>.GetTool("ScoreSystem");
        timerSystem = Tools<TimerSystem>.GetTool("TimerSystem");
        directorSystem = Tools<DirectorSystem>.GetTool("DirectorSystem");
        uiSystem = Tools<UISystem>.GetTool("UISystem");

        enemyPool = Tools<EnemyPool>.GetTool("EnemyPool");

        defencer = Tools<Defencer>.GetTool("Defencer");

        GameObject PointsParent = GameObject.Find("SpawnPoints");
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = PointsParent.transform.GetChild(i);
        }

        directorSystem.visualSystem.AfterTutorial = () => { timerSystem.TimerStart(60); StartCoroutine(RandomSpawn()); };
    }

    void Update()
    {
        DragScreen();
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
            if (timerSystem.timeUp)
            {
                int[] scoreChart = scoreSystem.GetScoreChart(1);
                directorSystem.visualSystem.ResultAnimation(scoreSystem.GetScore(), scoreChart, true);
                uiSystem.IsUIOpened = true;
                yield break;
            }
            else if (timerSystem.GetTime() <= 20)
            {
                spawnTime = 1f;
            }
            else if (timerSystem.GetTime() <= 40)
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
