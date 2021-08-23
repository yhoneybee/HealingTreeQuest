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

    public bool isTutorial = true;
    public delegate void Tutorials();
    public Tutorials AfterTutorial;

    public GameObject[] tutorialTexts;
    int tutorialIndex = 0;

    void Start()
    {
        FadeIn(GameObject.Find("Fade"), 0.5f);
        tutorialTexts[tutorialIndex].SetActive(true);
        FadeIn(tutorialTexts[tutorialIndex], 1);
    }

    void Update()
    {
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
                AfterTutorial();
                return;
            }
            FadeIn(tutorialTexts[tutorialIndex], 1);
            tutorialTexts[tutorialIndex].SetActive(true);
        }
    }
    public void ResultAnimation(int score, int[] scoreChart)
    {
        resultWindow[0].title[0].transform.parent.gameObject.SetActive(true);
        List<GameObject> titles = resultWindow[0].title;
        List<GameObject> backgrounds = resultWindow[0].background;
        List<GameObject> contents = resultWindow[0].content;

        contents[0].GetComponent<Button>().onClick.AddListener(() => { DDOLObj.Instance.GameClear(); });

        contents[1].GetComponent<Text>().text =
            $"Score : {score}\nBest: {scoreChart[0]}\n\n1 : {scoreChart[0]}\n2 : {scoreChart[1]}\n3 : {scoreChart[2]}";

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
            FadeIn(resultWindow[0].content[i], 1);
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

    public void SizeEffect(GameObject obj)
    {
        StartCoroutine(_SizeEffect(obj));
    }

    IEnumerator _SizeEffect(GameObject obj)
    {
        while (true)
        {
            if (obj.transform.localScale.x <= 0.81)
            {
                obj.transform.localScale = new Vector2(0.8f, 0.8f);
                break;
            }
            obj.transform.localScale = Vector2.Lerp(obj.transform.localScale, new Vector2(0.8f, 0.8f), 0.6f);
            yield return new WaitForSeconds(0.005f);
        }

        while (true)
        {
            if (obj.transform.localScale.x >= 1.19f)
            {
                obj.transform.localScale = new Vector2(1.2f, 1.2f);
                break;
            }
            obj.transform.localScale = Vector2.Lerp(obj.transform.localScale, new Vector2(1.2f, 1.2f), 0.6f);
            yield return new WaitForSeconds(0.005f);
        }

        while (true)
        {
            if (obj.transform.localScale.x <= 1.01f)
            {
                obj.transform.localScale = Vector2.one;
                break;
            }
            obj.transform.localScale = Vector2.Lerp(obj.transform.localScale, Vector2.one, 0.6f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
