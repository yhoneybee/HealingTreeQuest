using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 과일 수확
public class GameSystem3 : MonoBehaviour
{
    public ScoreSystem scoreSystem { get; set; }
    TimerSystem timerSystem;
    public DirectorSystem directorSystem { get; set; }
    public UISystem uiSystem { get; set; }

    public Slider moveBar;

    [SerializeField] GameObject fadeA;
    [SerializeField] GameObject fadeB;
    GameObject[] fruit;

    Text targetScore;

    int clearScore = 7000;

    void Start()
    {
        directorSystem = Tools<DirectorSystem>.GetTool("DirectorSystem");
        scoreSystem = Tools<ScoreSystem>.GetTool("ScoreSystem");
        timerSystem = Tools<TimerSystem>.GetTool("TimerSystem");
        uiSystem = Tools<UISystem>.GetTool("UISystem");

        moveBar = Tools<Slider>.GetTool("MoveBar");
        targetScore = Tools<Text>.GetTool("TargetScore");
        targetScore.text = clearScore.ToString();

        fruit = Resources.LoadAll<GameObject>("Prefabs/MiniGame/과일 수확");

        directorSystem.visualSystem.TutorialOfIndex = new VisualSystem.Tutorials[directorSystem.visualSystem.tutorialTexts.Length];
        directorSystem.visualSystem.TutorialOfIndex[2] = () => { fadeA.SetActive(true); directorSystem.visualSystem.FadeIn(fadeA, 0.5f); };
        directorSystem.visualSystem.TutorialOfIndex[4] = () => { fadeA.SetActive(false); fadeB.SetActive(true); directorSystem.visualSystem.FadeIn(fadeB, 0.5f); };
        directorSystem.visualSystem.TutorialOfIndex[5] = () => { fadeB.SetActive(false); };
        directorSystem.visualSystem.AfterTutorial = () => { timerSystem.TimerStart(60); StartCoroutine(RandomSpawn()); };
    }

    IEnumerator RandomSpawn()
    {
        float spawnTime = 2f;
        while (true)
        {
            if (timerSystem.timeUp)
            {
                bool gameClear = scoreSystem.GetScore() >= clearScore;

                int[] scoreChart = scoreSystem.GetScoreChart(2);
                directorSystem.isGameEnd = true;
                directorSystem.visualSystem.ResultAnimation(scoreSystem.GetScore(), scoreChart, gameClear);
                uiSystem.IsUIOpened = true;

                GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
                foreach (GameObject fruit in fruits)
                {
                    Destroy(fruit);
                }
                yield break;
            }

            else if (timerSystem.GetTime() <= 20)
                spawnTime = 0.1f;
            else if (timerSystem.GetTime() <= 40)
                spawnTime = 0.5f;

            yield return new WaitForSeconds(spawnTime);
            GameObject obj = Instantiate(fruit[Random.Range(0, 3)], new Vector2(Random.Range(-2.2f, 2.2f), 4), Quaternion.identity);
            obj.transform.localScale = new Vector2(0.25f, 0.25f);
            obj.GetComponent<Rigidbody2D>().gravityScale = 0;
            while (true)
            {
                obj.transform.localScale = Vector2.Lerp(obj.transform.localScale, Vector2.one, 0.2f);
                yield return new WaitForSeconds(0.01f);
                if (obj.transform.localScale.x >= 0.99f)
                {
                    obj.transform.localScale = Vector2.one;
                    obj.GetComponent<Rigidbody2D>().gravityScale = 1;
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            Fruit fruit = collision.GetComponent<Fruit>();
            scoreSystem.ScoreMinus(fruit.Score);
            uiSystem.TextAnim($"- {fruit.Score}");
            StartCoroutine(DestroyFruit(fruit.gameObject));
        }
    }

    IEnumerator DestroyFruit(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(obj.GetComponent<Rigidbody2D>());
        yield return new WaitForSeconds(1f);
        Destroy(obj);
    }
}
