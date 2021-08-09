using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem1 : MonoBehaviour
{
    List<Weeds> weeds = new List<Weeds>();
    Text scoreText;
    Sprite[] sprites;
    GameObject[] spawnPoints;
    ScoreSystem scoreSystem;
    UISystem uiSystem;
    void Start()
    {
        scoreSystem = Tools<ScoreSystem>.GetTool("ScoreSystem");
        uiSystem = Tools<UISystem>.GetTool("UISystem");

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        sprites = Resources.LoadAll<Sprite>("Sprites/MiniGame/잡초 뽑기/잡초");

        scoreText = Tools<Text>.GetTool("ScoreText");

        InvokeRepeating("RandomSpawn", 0, 2);
    }

    void Update()
    {
        for (int i = 0; i < weeds.Count; i++)
        {
            if (weeds[i].slider.value >= 4f)
            {
                weeds[i].Release();
                weeds.Remove(weeds[i]);
                continue;
            }
            weeds[i].GetComponent<Image>().sprite = sprites[(int)weeds[i].slider.value];
        }
    }

    void RandomSpawn()
    {
        int random;
        Image image;

        do {
            random = Random.Range(0, 9);
            image = spawnPoints[random].GetComponent<Image>();
        } while (image.sprite != null);

        image.sprite = sprites[0];
        Weeds weed = spawnPoints[random].GetComponent<Weeds>();
        weeds.Add(weed);
        weed.Init(spawnPoints[random]);
    }
}
