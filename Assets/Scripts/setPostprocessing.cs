using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class setPostprocessing : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] Image img;
    [SerializeField] Text txt;
    [SerializeField] GameObject TitleCanvas;
    public PostProcessVolume ppVolume;

    public bool isTouch;
    IEnumerator UpdateCoru;

    private void Awake()
    {
        if (DDOLObj.Instance.tempObj)
        {
            Canvas.SetActive(true);
            TitleCanvas.SetActive(false);
            Destroy(gameObject);
            return;
        }
        ppVolume = GetComponent<PostProcessVolume>();
        UpdateCoru = TxtEffect();
        StartCoroutine(UpdateCoru);
    }
    void Update()
    {
        if (DDOLObj.Instance.tempObj) return;
        if (Input.GetMouseButtonDown(0) && !isTouch)
        {
            DDOLObj.Instance.tempObj = DDOLObj.Instance;
            isTouch = true;
            StopCoroutine(UpdateCoru);
            StartCoroutine("setScreen");
            StartCoroutine("setPosition");
        }
    }

    IEnumerator TxtEffect()
    {
        bool isUp = false;
        while (true)
        {
            if (!isUp)
            {
                txt.color -= new Color(0, 0, 0, 0.01f);
                if (txt.color.a <= 0.5f)
                    isUp = true;
            }
            else
            {
                txt.color += new Color(0, 0, 0, 0.01f);
                if (txt.color.a >= 1)
                    isUp = false;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator setScreen()
    {
        while (true)
        {
            ppVolume.weight -= 0.01f;
            img.color -= new Color(0, 0, 0, 0.01f);
            txt.color -= new Color(0, 0, 0, 0.01f);
            if (ppVolume.weight <= 0)
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator setPosition()
    {
        UiManager UI = UiManager.Instance;


        while (true)
        {
            UI.Title_distance = Mathf.Lerp(UI.Title_distance, 0, Time.deltaTime * 3);
            if (UI.Title_distance <= 0.1f)
            {
                TitleCanvas.SetActive(false);
                Canvas.SetActive(true);
                UI.Title_distance = 0;
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}
