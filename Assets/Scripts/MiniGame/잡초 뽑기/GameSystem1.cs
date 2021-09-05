using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ¿‚√  ªÃ±‚
public class GameSystem1 : MonoBehaviour
{
    Sprite[] w_sprites;
    Sprite[] f_sprites;

    GameObject[] spawnPoints;
    public GameObject fadeObj;
    public GameObject weedsUI;
    public GameObject flowerUI;

    [HideInInspector]
    public ScoreSystem scoreSystem { get; set; }
    public DirectorSystem directorSystem { get; set; }
    public UISystem uiSystem { get; set; }
    TimerSystem timeSystem;

    float spawnTime = 1f;
    bool gameFinish = false;

    void Start()
    {
        scoreSystem = Tools<ScoreSystem>.GetTool("ScoreSystem");
        timeSystem = Tools<TimerSystem>.GetTool("TimeSystem");
        directorSystem = Tools<DirectorSystem>.GetTool("DirectorSystem");
        uiSystem = Tools<UISystem>.GetTool("UISystem");

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        w_sprites = Resources.LoadAll<Sprite>("Sprites/MiniGame/¿‚√  ªÃ±‚/¿‚√ ");
        f_sprites = Resources.LoadAll<Sprite>("Sprites/MiniGame/¿‚√  ªÃ±‚/≤…");

        directorSystem.visualSystem.TutorialOfIndex = new VisualSystem.Tutorials[directorSystem.visualSystem.tutorialTexts.Length];
        directorSystem.visualSystem.TutorialOfIndex[2] = () => { fadeObj.SetActive(true); directorSystem.visualSystem.FadeIn(fadeObj, 0.5f); };
        directorSystem.visualSystem.TutorialOfIndex[4] = () =>
        {
            fadeObj.SetActive(false);

            weedsUI.SetActive(true);
            directorSystem.visualSystem.FadeIn(weedsUI, 1);
        };
        directorSystem.visualSystem.TutorialOfIndex[5] = () =>
        {
            weedsUI.SetActive(false);

            flowerUI.SetActive(true);
            directorSystem.visualSystem.FadeIn(flowerUI, 1);
        };
        directorSystem.visualSystem.TutorialOfIndex[6] = () => { flowerUI.SetActive(false); };

        directorSystem.visualSystem.AfterTutorial = () => { timeSystem.TimerStart(60); StartCoroutine(RandomSpawn()); };
    }

    IEnumerator RandomSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            int random = 0;
            Image image = null;

            int count = 0;
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i].GetComponent<Image>().enabled)
                    count++;
            }
            do
            {
                if (count >= spawnPoints.Length) break;
                random = Random.Range(0, spawnPoints.Length);
                image = spawnPoints[random].GetComponent<Image>();
            } while (image.enabled);

            if (!(count >= spawnPoints.Length))
            {
                Weeds weed = spawnPoints[random].GetComponent<Weeds>();
                weed.GetComponent<RectTransform>().transform.localScale = new Vector2(0.5f, 0.5f);
                weed.Init(spawnPoints[random]);
                int randomValue = Random.Range(0, 5);
                if (randomValue == 4)
                {
                    weed.sprites = f_sprites;
                    weed.isFlower = true;
                }
                else
                {
                    weed.sprites = w_sprites;
                    weed.isFlower = false;
                }
            }

            if (timeSystem.timeUp)
            {
                gameFinish = true;

                bool gameClear = scoreSystem.GetScore() >= 15000;

                directorSystem.isGameEnd = true;
                int[] scoreChart = scoreSystem.GetScoreChart(0);
                directorSystem.visualSystem.ResultAnimation(scoreSystem.GetScore(), scoreChart, gameClear);
                yield break;
            }
            else if (timeSystem.GetTime() <= 20)
            {
                spawnTime = 0.2f;
            }
            else if (timeSystem.GetTime() <= 40)
            {
                spawnTime = 0.5f;
            }
        }
    }

    public void ClickWeeds(Weeds weeds)
    {
        Image image = weeds.GetComponent<Image>();
        if (!image.sprite) return;

        weeds.Release();

        if (weeds.isFlower)
        {
            scoreSystem.ScoreMinus(100);
            uiSystem.TextAnim("- 100");
        }
        else
        {
            scoreSystem.ScorePlus(100);
            uiSystem.TextAnim("+ 100");
        }
    }
}
