using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class setPostprocessing : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] Image img;
    [SerializeField] GameObject TitleCanvas;
    public PostProcessVolume ppVolume;

    public bool isTouch;

    private void Awake()
    {
        ppVolume = GetComponent<PostProcessVolume>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTouch)
        {
            isTouch = true;
            StartCoroutine("setScreen");
            StartCoroutine("setPosition");
        }
    }

    IEnumerator setScreen()
    {
        while (true)
        {
            ppVolume.weight -= 0.01f;
            img.color -= new Color(0, 0, 0, 0.01f);
            if (ppVolume.weight <= 0)
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator setPosition()
    {
        float second = 0.01f;
        
        while (true)
        {
            UiManager.Instance.Title_distance -= (0.1f);
            second += 0.0007f;
            if (UiManager.Instance.Title_distance <= 0)
            {
                TitleCanvas.SetActive(false);
                Canvas.SetActive(true);
                break;
            }

            yield return new WaitForSeconds(second);
        }
    }
}
