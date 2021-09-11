using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualSystem : MonoBehaviour
{
    [System.Serializable]
    private struct ResultWindow
    {
        public List<GameObject> title;
        public List<GameObject> background;
        public List<GameObject> content;
    }
    [SerializeField]
    private ResultWindow[] resultWindow;
    [SerializeField] Sprite clearBG;
    [SerializeField] Sprite failBG;

    public bool isTutorial = true;
    public delegate void Tutorials();
    public Tutorials AfterTutorial;
    public Tutorials[] TutorialOfIndex;

    public GameObject[] tutorialTexts;
    int tutorialIndex = 0;

    void Start()
    {
        if (PlayerPrefs.HasKey(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name))
        {
            GameObject.Find("Fade").SetActive(false);
            isTutorial = false;
            return;
        }
        FadeIn(GameObject.Find("Fade"), 0.5f);

        tutorialTexts[tutorialIndex].SetActive(true);
        FadeIn(tutorialTexts[tutorialIndex], 1);

        PlayerPrefs.SetString(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, "isTutorialed");
    }

    void Update()
    {
        if (!isTutorial && AfterTutorial != null)
        {
            AfterTutorial();
            AfterTutorial = null;
        }
        if (isTutorial)
            Tutorial();
    }
    void Tutorial()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tutorialTexts[tutorialIndex].SetActive(false);
            tutorialIndex++;
            if (tutorialIndex >= tutorialTexts.Length)
            {
                isTutorial = false;
                FadeOut(GameObject.Find("Fade"), 0);
                return;
            }
            if (TutorialOfIndex != null)
                if (TutorialOfIndex[tutorialIndex] != null)
                    TutorialOfIndex[tutorialIndex]();

            FadeIn(tutorialTexts[tutorialIndex], 1);
            tutorialTexts[tutorialIndex].SetActive(true);
        }
    }
    public void ResultAnimation(int score, int[] scoreChart, bool gameClear)
    {
        List<GameObject> titles = resultWindow[0].title;
        List<GameObject> backgrounds = resultWindow[0].background;
        List<GameObject> contents = resultWindow[0].content;

        if (gameClear)
        {
            contents[1].GetComponent<Button>().onClick.AddListener(() => { DDOLObj.Instance.GameClear(); });
            backgrounds[0].GetComponent<Image>().sprite = clearBG;
            SoundManager.Instance.Play("Clear", SoundType.EFFECT);
        }
        else
        {
            contents[1].GetComponent<Button>().onClick.AddListener(() =>
            { UnityEngine.SceneManagement.SceneManager.LoadScene("Ingame"); });
            backgrounds[0].GetComponent<Image>().sprite = failBG;
        }

        contents[0].GetComponent<Text>().text =
            $"{score}\n {scoreChart[0]}\n\n{scoreChart[0]}\n{scoreChart[1]}\n{scoreChart[2]}";

        for (int i = 0; i < titles.Count; i++)
        {
            ObjMove(titles[i], new Vector2(0, 510));
        }

        for (int i = 0; i < backgrounds.Count; i++)
        {
            ObjMove(backgrounds[i], Vector2.zero);
        }

        for (int i = 0; i < contents.Count; i++)
        {
            contents[i].SetActive(true);
            FadeIn(contents[i], 1);
        }
    }

    void ObjMove(GameObject obj, Vector2 target)
    {
        StartCoroutine(_ObjMove(obj, target));
    }

    IEnumerator _ObjMove(GameObject obj, Vector2 target)
    {
        while (true)
        {
            obj.transform.localPosition = Vector2.Lerp(obj.transform.localPosition, target, 0.3f);
            yield return new WaitForSeconds(0.01f);

            if (Vector2.Distance(obj.transform.localPosition, target) <= 0.1f)
            {
                obj.transform.localPosition = target;
                yield break;
            }
        }
    }

    public void FadeIn(GameObject obj, float alpha)
    {
        StartCoroutine(_FadeIn(obj, alpha));
    }
    IEnumerator _FadeIn(GameObject obj, float alpha)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        Image img = obj.GetComponent<Image>();
        Text txt = obj.GetComponent<Text>();

        Color c;
        if (sr)
            c = sr.color;
        else if (img)
            c = img.color;
        else if (txt)
            c = txt.color;
        else yield break;

        while (true)
        {
            c.a += 0.01f;
            if (c.a >= alpha) break;

            if (sr)
                sr.color = c;
            else if (img)
                img.color = c;
            else if (txt)
                txt.color = c;

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void FadeOut(GameObject obj, float alpha)
    {
        StartCoroutine(_FadeOut(obj, alpha));
    }

    IEnumerator _FadeOut(GameObject obj, float alpha)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        Image img = obj.GetComponent<Image>();
        Text txt = obj.GetComponent<Text>();

        Color c;
        if (sr)
            c = sr.color;
        else if (img)
            c = img.color;
        else if (txt)
            c = txt.color;
        else yield break;

        while (true)
        {
            c.a -= 0.01f;
            if (c.a <= alpha) break;

            if (sr)
                sr.color = c;
            else if (img)
                img.color = c;
            else if (txt)
                txt.color = c;

            yield return new WaitForSeconds(0.01f);
        }
        obj.SetActive(false);
    }

    public void SizeEffect(GameObject obj, Vector2 targetScale1, Vector2 targetScale2)
    {
        Vector2 curScale = obj.transform.localScale;
        StartCoroutine(_SizeEffect(obj, curScale, targetScale1, targetScale2));
    }

    IEnumerator _SizeEffect(GameObject obj, Vector2 curScale, Vector2 targetScale1, Vector2 targetScale2)
    {
        while (true)
        {
            if (obj.transform.localScale.x <= targetScale1.x + 0.1f)
            {
                obj.transform.localScale = targetScale1;
                break;
            }
            obj.transform.localScale = Vector2.Lerp(obj.transform.localScale, targetScale1, 0.3f);
            yield return new WaitForSeconds(0.005f);
        }

        while (true)
        {
            if (obj.transform.localScale.x >= targetScale1.x - 0.1f)
            {
                obj.transform.localScale = targetScale2;
                break;
            }
            obj.transform.localScale = Vector2.Lerp(obj.transform.localScale, targetScale2, 0.6f);
            yield return new WaitForSeconds(0.005f);
        }

        while (true)
        {
            if (obj.transform.localScale.x <= curScale.x + 0.1f)
            {
                obj.transform.localScale = curScale;
                break;
            }
            obj.transform.localScale = Vector2.Lerp(obj.transform.localScale, curScale, 0.6f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
