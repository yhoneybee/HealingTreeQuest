using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trash : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    GameSystem4 gameSystem;
    new RectTransform transform;

    public int id;
    public float destroyTime { get; set; }
    bool mouseUp = false;
    bool isCollision = false;

    void Start()
    {
        transform = GetComponent<RectTransform>();
        StartCoroutine(SpawnAni());
        gameSystem = Tools<GameSystem4>.GetTool("GameSystem");

        Invoke("FallTrash", destroyTime);
    }
    void Update()
    {
        if (gameSystem.directorSystem.isGameEnd) Destroy(gameObject);

        if (transform.localPosition.y <= -1375)
        {
            DestroyTrash(false);
        }
    }
    IEnumerator SpawnAni()
    {
        float scale_x = transform.localScale.x;
        float scale_y = transform.localScale.y;
        while (true)
        {
            scale_x = scale_y = Mathf.Lerp(scale_x, 1, 0.3f);
            transform.localScale = new Vector2(scale_x, scale_y);
            if (scale_x >= 0.9f)
                break;
            yield return new WaitForSeconds(0.01f);
        }
        transform.localScale = Vector2.one;
    }
    void DestroyTrash(bool isPlus = false)
    {
        Destroy(gameObject);

        if (isPlus)
        {
            gameSystem.scoreSystem.ScorePlus(100);
            gameSystem.uiSystem.TextAnim("+ 100");
        }
        else
        {
            gameSystem.scoreSystem.ScoreMinus(50);
            gameSystem.uiSystem.TextAnim("- 50");
        }
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
    public void OnPointerClick(PointerEventData eventData)
    {
        mouseUp = true;
        if (!isCollision) FallTrash();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (transform.CompareTag("Trash") && collision.CompareTag("Trash Zone"))
        {
            isCollision = true;
            TrashZone trZone = collision.GetComponent<TrashZone>();
            trZone.GetComponent<UnityEngine.UI.Image>().sprite = trZone.openSprite;

            if (mouseUp)
            {
                if (id == collision.GetComponent<TrashZone>().id)
                    gameSystem.directorSystem.visualSystem.SizeEffect(collision.gameObject, new Vector2(0.8f, 0.8f), new Vector2(1.3f, 1.3f));

                DestroyTrash(id == collision.GetComponent<TrashZone>().id);
            }
        }
        else
        {
            if (mouseUp)
            {
                FallTrash();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (transform.CompareTag("Trash") && collision.CompareTag("Trash Zone"))
        {
            isCollision = false;
            TrashZone trZone = collision.GetComponent<TrashZone>();
            trZone.GetComponent<UnityEngine.UI.Image>().sprite = trZone.defaultSprite;
        }
    }

    void FallTrash()
    {
        GetComponent<Rigidbody2D>().gravityScale = 500;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<UnityEngine.UI.Image>().raycastTarget = false;
        tag = "Untagged";
    }
}