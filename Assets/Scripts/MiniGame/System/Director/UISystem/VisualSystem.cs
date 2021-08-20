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

    void Start()
    {
    }

    void Update()
    {

    }

    public void ResultAnimation(int score, int[] scoreChart)
    {
        List<GameObject> titles = resultWindow[0].title;
        List<GameObject> backgrounds = resultWindow[0].background;
        List<GameObject> contents = resultWindow[0].content;

        contents[1].GetComponent<Text>().text =
            $"Score : {score}\n\nBest: {scoreChart[0]}\n\n1 : {scoreChart[0]}\n2 : {scoreChart[1]}\n3 : {scoreChart[2]}";

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
            FadeIn(resultWindow[0].content[i]);
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
            obj.transform.localPosition = Vector2.Lerp(obj.transform.localPosition, target, 0.2f);
            yield return new WaitForSeconds(0.01f);

            if (Vector2.Distance(obj.transform.localPosition, target) <= 0.1f)
            {
                obj.transform.localPosition = target;
                yield break;
            }
        }
    }

    public void FadeIn(GameObject obj)
    {
        StartCoroutine(_FadeIn(obj));
    }
    IEnumerator _FadeIn(GameObject obj)
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

            if (sr)
                sr.color = c;
            else if (img)
                img.color = c;
            else if (txt)
                txt.color = c;

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void FadeOut(GameObject obj)
    {
        StartCoroutine(_FadeOut(obj));
    }

    IEnumerator _FadeOut(GameObject obj)
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

            if (sr)
                sr.color = c;
            else if (img)
                img.color = c;
            else if (txt)
                txt.color = c;

            yield return new WaitForSeconds(0.01f);
        }
    }
}