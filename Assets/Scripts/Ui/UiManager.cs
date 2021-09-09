using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; } = null;

    public Camera Cam => Camera.main;
    public Transform CamTf => Cam.transform;

    public List<UiObj> UiObjs = new List<UiObj>();
    public List<RectTransform> TutorialTargets = new List<RectTransform>();
    public List<string> TutorialInfo = new List<string>();
    public Image TutorialSelectImg;
    public Image ClickBlockingImg;
    public RectTransform Canvas;
    public UiObj Menu;

    [SerializeField] Image previewImg;
    [SerializeField] Sprite[] previewSprites;
    [SerializeField] RectTransform circleTr;
    [SerializeField] Text plusText;

    Coroutine circleAnim;
    Coroutine textAnim;
    Coroutine CRotate = null;

    public Vector3 MouseCenterPos => Cam.ScreenToViewportPoint(Input.mousePosition);

    //public Vector3 WoodPos = new Vector3(0, 0.57f, 0);
    public Transform Wood;

    Vector2 Now;
    Vector2 Prev;
    Vector2 xy_axis;
    Vector2 touch_1, touch_2;

    public float Distance = 10;
    float max_x = 0;
    float touch_distance = 0;
    public float Title_distance;

    private bool custom_mode = false;
    public bool CustomMode
    {
        get { return custom_mode; }
        set { custom_mode = value; }
    }

    bool preview = false;
    public bool Preview
    {
        get => preview;
        set
        {
            preview = value;

            previewImg.sprite = preview ? previewSprites[1] : previewSprites[0];
            if (circleAnim != null) StopCoroutine(circleAnim);
            circleAnim = StartCoroutine(CircleAnim(preview));
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("First"))
        {
            StartCoroutine(ETutorialStart());
            PlayerPrefs.SetInt("First", 1);
        }
    }
    private void Update()
    {
        CamTf.transform.position = new Vector3(Wood.position.x, 4.5f, Wood.position.z- Title_distance);

        Distance = Mathf.Lerp(Distance, Wood.localScale.x * (FindObjectOfType<Tree>().Level + 1 * 10) / 140, Time.deltaTime * 3);
        CamTf.Translate(new Vector3(0, 0, -Distance));

        if (Preview)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Prev = MouseCenterPos;
            }
            else if (Input.GetMouseButton(0))
            {
                if (Input.touchCount == 1)
                {
                    touch_1 = Input.GetTouch(0).position;

                    Now = MouseCenterPos;
                    Vector3 dir = Prev - Now;

                    xy_axis = new Vector2(-dir.x * 100, dir.y * 100);

                    if (Mathf.Abs(max_x) < Mathf.Abs(xy_axis.x))
                    {
                        max_x = xy_axis.x;
                        print(max_x);
                    }

                    CamTf.transform.position = new Vector3(Wood.position.x, 4.5f, Wood.position.z);

                    CamTf.Rotate(Vector3.up, xy_axis.x, Space.World);
                    CamTf.Rotate(Vector3.right, xy_axis.y);

                    if (CamTf.localEulerAngles.x > 300)
                        CamTf.localEulerAngles = new Vector3(0, CamTf.localEulerAngles.y, CamTf.localEulerAngles.z);
                    else if (CamTf.localEulerAngles.x > 30)
                        CamTf.localEulerAngles = new Vector3(30, CamTf.localEulerAngles.y, CamTf.localEulerAngles.z);

                    CamTf.Translate(new Vector3(0, 0, -Distance));

                    Prev = Now;
                }
                else if (Input.touchCount == 2)
                {
                    touch_2 = Input.GetTouch(1).position;

                    if (Vector2.Distance(touch_1, touch_2) < touch_distance && /*Cam.fieldOfView - 3 > 30 &&*/ Cam.fieldOfView + 3 < 130)
                        Cam.fieldOfView += 3;
                    else if (Vector2.Distance(touch_1, touch_2) > touch_distance && Cam.fieldOfView - 3 > 30 /*&& Cam.fieldOfView + 3 < 130*/)
                        Cam.fieldOfView -= 3;

                    touch_distance = Vector2.Distance(touch_1, touch_2);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (CRotate != null) StopCoroutine(CRotate);
                CRotate = StartCoroutine(ERotate());
            }
        }
    }

    public void SwitchRaycastTargetMode(bool value)
    {
        UiObj child;
        for (int i = 0; i < Menu.RectTransform.childCount; i++)
        {
            child = Menu.RectTransform.GetChild(i).GetComponent<UiObj>();
            child.Image.raycastTarget = value;
        }
    }
    public void TextAnim(int value)
    {
        if (textAnim != null) StopCoroutine(textAnim);
        textAnim = StartCoroutine(_TextAnim(value));
    }

    IEnumerator ERotate()
    {
        for (int i = 0; i < 200; i++)
        {
            CamTf.transform.position = new Vector3(Wood.position.x, 4.5f, Wood.position.z);

            max_x = Mathf.Lerp(max_x, 0, Time.deltaTime);

            CamTf.Rotate(Vector3.up, Time.deltaTime * max_x, Space.World);

            CamTf.Translate(new Vector3(0, 0, -Distance));
            yield return new WaitForSeconds(0.001f);
        }

        yield return null;
    }
    IEnumerator CircleAnim(bool isOn)
    {
        Vector2 targetPos = new Vector2(-55, 0);
        if (isOn) targetPos = new Vector2(55, 0);
        while (true)
        {
            if (Vector2.Distance(circleTr.localPosition, targetPos) <= 0.1f)
            {
                circleTr.localPosition = targetPos;
                yield break;
            }
            yield return new WaitForSeconds(0.01f);
            circleTr.localPosition = Vector2.Lerp(circleTr.localPosition, targetPos, 0.5f);
        }
    }
    IEnumerator _TextAnim(int value)
    {
        plusText.gameObject.SetActive(true);
        plusText.text = $"+ {value}";

        yield return new WaitForSeconds(1);
        while (true)
        {
            if (plusText.color.a <= 0) break;
            yield return new WaitForSeconds(0.01f);
            plusText.color = new Color(plusText.color.r, plusText.color.g, plusText.color.b, plusText.color.a - 0.005f);
        }

        plusText.gameObject.SetActive(false);
        plusText.color = plusText.color + new Color(0, 0, 0, 1);
    }
    IEnumerator ETutorialStart()
    {
        ClickBlockingImg.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        var UIInfo = ClickBlockingImg.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        UIInfo.text = $"Healing Tree Quest에 오신 것을 환영합니다! 터치하시면 튜토리얼을 시작합니다!";

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UIInfo.text = "";
                break;
            }
            else yield return null;
        }


        for (int i = 0; i < TutorialTargets.Count;)
        {
            print(i);
            UIInfo.text = "";

            var rt = TutorialSelectImg.GetComponent<RectTransform>();

            UIInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, rt.position.y + rt.rect.height / 2 + UIInfo.GetComponent<RectTransform>().rect.height / 2);

            rt.sizeDelta = TutorialTargets[i].sizeDelta;
            rt.anchorMin = TutorialTargets[i].anchorMin;
            rt.anchorMax = TutorialTargets[i].anchorMax;
            rt.pivot = TutorialTargets[i].pivot;
            rt.position = TutorialTargets[i].position;

            while (TutorialSelectImg.color.a < 0.3568628f - 0.05f)
            {
                TutorialSelectImg.color = Color.Lerp(TutorialSelectImg.color, new Color(1, 1, 1, 0.3568628f), Time.deltaTime * 3);
                yield return new WaitForSeconds(0.001f);
            }
            UIInfo.text = TutorialInfo[i];
            TutorialSelectImg.color = new Color(1, 1, 1, 0.3568628f);

            if (Input.GetMouseButtonDown(0))
            {
                UIInfo.text = "";
                while (TutorialSelectImg.color.a > 0.05f)
                {
                    TutorialSelectImg.color = Color.Lerp(TutorialSelectImg.color, new Color(1, 1, 1, 0), Time.deltaTime * 3);
                    yield return new WaitForSeconds(0.001f);
                }
                TutorialSelectImg.color = new Color(1, 1, 1, 0);
                i++;
            }
            else
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(1);

        UIInfo.text = $"수고하셨습니다! 튜토리얼을 종료합니다, 즐겁게 즐겨주세요~!";

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
                break;
            else
                yield return null;
        }

        ClickBlockingImg.gameObject.SetActive(false);

        yield return null;
    }
}
