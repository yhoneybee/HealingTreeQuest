using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    GameSystem3 gameSystem;
    float moveRange;

    Vector2 targetPos = new Vector2(2.3f, -1.5f);
    void Start()
    {
        gameSystem = Tools<GameSystem3>.GetTool("GameSystem");
    }

    void Update()
    {
        moveRange = gameSystem.moveBar.value;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPos.x * moveRange, targetPos.y), 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            gameSystem.directorSystem.visualSystem.SizeEffect(gameObject);
            gameSystem.scoreSystem.ScorePlus(100);
            Destroy(collision.gameObject);
        }
    }


}
