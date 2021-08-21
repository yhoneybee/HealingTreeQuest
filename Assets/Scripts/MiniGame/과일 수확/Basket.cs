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
            StartCoroutine(SizeEffect());
            gameSystem.scoreSystem.ScorePlus(100);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator SizeEffect()
    {
        while (true)
        {
            if (transform.localScale.x <= 0.81)
            {
                transform.localScale = new Vector2(0.8f, 0.8f);
                break;
            }
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0.8f, 0.8f), 0.6f);
            yield return new WaitForSeconds(0.005f);
        }

        while (true)
        {
            if (transform.localScale.x >= 1.19f)
            {
                transform.localScale = new Vector2(1.2f, 1.2f);
                break;
            }
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1.2f, 1.2f), 0.6f);
            yield return new WaitForSeconds(0.005f);
        }

        while (true)
        {
            if (transform.localScale.x <= 1.01f)
            {
                transform.localScale = Vector2.one;
                break;
            }
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, 0.6f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
