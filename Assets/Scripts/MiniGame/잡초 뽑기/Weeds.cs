using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Weeds : MonoBehaviour
{
    GameSystem1 gameSystem;
    public bool isSpawned = false;
    public bool isFlower;
    public Sprite[] sprites;
    Slider slider;
    Image image;

    private void Start()
    {
        gameSystem = Tools<GameSystem1>.GetTool("GameSystem");
        image = GetComponent<Image>();
        image.enabled = false;
    }
    void Update()
    {
        if (gameSystem.directorSystem.isGameEnd) Destroy(gameObject);
        if (isSpawned)
        {
            slider.value += Time.deltaTime;
            if (slider.value >= 4f)
            {
                Release();
                if (isFlower)
                {
                    gameSystem.scoreSystem.ScorePlus(100);
                    gameSystem.uiSystem.TextAnim("+ 100");
                }
                else
                {
                    gameSystem.scoreSystem.ScoreMinus(100);
                    gameSystem.uiSystem.TextAnim("- 100");
                }
            }
            else
                image.sprite = sprites[(int)slider.value];
        }

    }
    public void Init(GameObject obj)
    {
        Slider tempSlider = Resources.Load<Slider>("Prefabs/Minigame/잡초 뽑기/Guage");
        slider = Instantiate(tempSlider, obj.transform);
        slider.transform.localPosition = new Vector2(0, 150);
        isSpawned = true;
        GetComponent<Image>().enabled = true;
        StartCoroutine(SizeUp());
    }
    IEnumerator SizeUp()
    {
        RectTransform transform = GetComponent<RectTransform>();
        while (true)
        {
            if (transform.localScale.x >= 0.99f)
            {
                transform.localScale = Vector2.one;
                yield break;
            }
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, 0.5f);
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void Release()
    {
        if (!isSpawned)
        {
            Debug.Log("없는 애를 지우려 하지 마셈");
            return;
        }

        isSpawned = false;
        Destroy(transform.GetChild(0).gameObject);
        GetComponent<Image>().enabled = false;
    }
}
