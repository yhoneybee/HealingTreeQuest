using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trash : MonoBehaviour, IDragHandler
{
    GameSystem4 gameSystem;
    public int ID { get; private set; }

    public float destroyTime { get; set; }

    void Start()
    {
        StartCoroutine(SpawnAni());
        gameSystem = Tools<GameSystem4>.GetTool("GameSystem");
        ID = Random.Range(0, gameSystem.spawnPoints.Length);
        var text = gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
        text.text = (ID + 1).ToString();

        Invoke("Destroy", destroyTime);
    }

    IEnumerator SpawnAni()
    {
        float scale_x = transform.localScale.x;
        float scale_y = transform.localScale.y;
        while(true)
        {
            scale_x = scale_y = Mathf.Lerp(scale_x, 1, 0.3f);

            transform.localScale = new Vector2(scale_x, scale_y);

            if(scale_x >= 0.9f)
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        transform.localScale = Vector2.one;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Input.mousePosition;
        transform.position = mousePos;

        if (transform.position.x >= 1080f) transform.position = new Vector2(1080f, transform.position.y);
        else if (transform.position.x <= 0) transform.position = new Vector2(0, transform.position.y);

        if (transform.position.y >= 1920f) transform.position = new Vector2(transform.position.x, 1920f);
        else if (transform.position.y <= 0) transform.position = new Vector2(transform.position.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trash Zone"))
        {
            if (ID == collision.GetComponent<TrashZone>().ID)
                gameSystem.scoreSystem.ScorePlus(100);
            else
                gameSystem.scoreSystem.ScoreMinus(50);

            Destroy(gameObject);
        }
    }
}
