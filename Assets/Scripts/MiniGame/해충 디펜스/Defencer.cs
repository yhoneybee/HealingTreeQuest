using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defencer : MonoBehaviour
{
    GameSystem2 gameSystem;
    public Direction direction;

    void Start()
    {
        gameSystem = Tools<GameSystem2>.GetTool("GameSystem");
        SetDir(Direction.Up);
    }

    void Update()
    {
    }

    public void SetDir(Direction dir)
    {
        direction = dir;

        switch (direction)
        {
            case Direction.Left:
                transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
                break;
            case Direction.Up:
                transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                break;
            case Direction.Down:
                transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameSystem.scoreSystem.ScoreMinus(50);
            gameSystem.enemyPool.ReleaseEnemy(collision.GetComponent<Enemy>());
            gameSystem.directorSystem.cameraSystem.ShakeCam(1, 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameSystem.scoreSystem.ScorePlus(100);
            gameSystem.enemyPool.ReleaseEnemy(collision.gameObject.GetComponent<Enemy>());
        }
    }
}
