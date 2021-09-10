using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public static RectTransform Dragging = null;
    public static UiObj Overring = null;

    public UiManager UI => UiManager.Instance;

    public Sprite OriginSprite;
    public Sprite ChangeSprite;

    public Image Image = null;
    public Coroutine CMoving = null;
    public Coroutine CPreview = null;
    public Coroutine CSwitchingHide = null;
    public Coroutine CScalingForChild = null;
    public RectTransform RectTransform = null;
    public GridLayoutGroup GridLayoutGroup = null;

    public Vector2 MousePos => Input.mousePosition;
    //public Vector2 MousePos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public Vector2 BeginMousePos;
    public Vector2 UiPos;

    private bool hide;
    public bool Hide
    {
        get { return hide; }
        set
        {
            hide = value;
            if (CSwitchingHide != null) StopCoroutine(CSwitchingHide);
            CSwitchingHide = StartCoroutine(ESwitchingHide());
        }
    }
    private bool preview;
    public bool Preview
    {
        get { return preview; }
        set
        {
            preview = value;
            if (CPreview != null) StopCoroutine(CPreview);
            CPreview = StartCoroutine(EPreview());
        }
    }

    public bool Moveable;
    public bool Compositeable = true;
    float cell_size = 0, spacing = 0, padding = 0;

    void Start()
    {
        if (GridLayoutGroup)
        {
            Hide = false;

            cell_size = GridLayoutGroup.cellSize.y;
            spacing = GridLayoutGroup.spacing.y;
            padding = 150;
            RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, (cell_size + spacing) * GetComponent<RectTransform>().childCount + padding);
        }
    }
    void Update()
    {
    }

    IEnumerator EMoving()
    {
        if (GridLayoutGroup)
        {
            float half = RectTransform.sizeDelta.y / 2, canvas_half = UI.Canvas.sizeDelta.y / 2;
            while (RectTransform.anchoredPosition.y > canvas_half - half + 0.05f)//À§
            {
                RectTransform.anchoredPosition = Vector2.Lerp(RectTransform.anchoredPosition, new Vector2(RectTransform.anchoredPosition.x, canvas_half - half), Time.deltaTime * 10);
                yield return new WaitForSeconds(0.001f);
            }
            while (RectTransform.anchoredPosition.y < -canvas_half + half - 0.05f)//¹Ø
            {
                RectTransform.anchoredPosition = Vector2.Lerp(RectTransform.anchoredPosition, new Vector2(RectTransform.anchoredPosition.x, -canvas_half + half), Time.deltaTime * 10);
                yield return new WaitForSeconds(0.001f);
            }
        }

        yield return null;
    }
    IEnumerator EPreview()
    {
        if (preview)
        {
            while (Image.color.a > 0)
            {
                Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, Image.color.a - 0.05f);
                yield return new WaitForSeconds(0.05f);
            }
            Image.enabled = !preview;
        }
        else
        {
            Image.enabled = !preview;
            while (1 > Image.color.a)
            {
                Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, Image.color.a + 0.05f);
                yield return new WaitForSeconds(0.05f);
            }
        }

        yield return null;
    }
    IEnumerator ESwitchingHide()
    {
        int to = Hide ? (RectTransform.anchorMin.x == 1 ? 0 : 1) : (RectTransform.anchorMin.x == 1 ? 1 : 0);

        while (true)
        {
            if (to == 0 && RectTransform.pivot.x < 0.05f) break;
            else if (to == 1 && RectTransform.pivot.x > 0.95f) break;

            RectTransform.pivot = Vector2.Lerp(RectTransform.pivot, new Vector2(to, RectTransform.pivot.y), Time.deltaTime * 10);
            yield return new WaitForSeconds(0.001f);
        }

        RectTransform.pivot = new Vector2(to, RectTransform.pivot.y);

        yield return null;
    }
    IEnumerator EScalingForChild(int child)
    {
        if (child == 0) yield break;
        while (true)
        {
            if (RectTransform.sizeDelta.y > (cell_size + spacing) * child + padding &&
                RectTransform.sizeDelta.y < (cell_size + spacing) * child + padding + 1)
                break;
            else if (RectTransform.sizeDelta.y < (cell_size + spacing) * child + padding &&
                     RectTransform.sizeDelta.y > (cell_size + spacing) * child + padding - 1)
                break;

            RectTransform.sizeDelta = Vector2.Lerp(RectTransform.sizeDelta, new Vector2(RectTransform.sizeDelta.x, (cell_size + spacing) * child + padding), Time.deltaTime * 10);
            yield return new WaitForSeconds(0.001f);
        }
        yield return null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (UI.CustomMode && Moveable)
        {
            Dragging = RectTransform;
            BeginMousePos = MousePos;
            UiPos = Dragging.position;
            Image.raycastTarget = false;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (UI.CustomMode && Moveable)
        {
            Vector2 delta = MousePos - BeginMousePos;

            delta += UiPos;

            Dragging.position = new Vector3(delta.x, delta.y, Dragging.position.z);

            if (Overring)
            {
                if (Overring.RectTransform.position.y > Dragging.position.y)
                    Dragging.SetSiblingIndex(Overring.RectTransform.GetSiblingIndex());
                else
                    Dragging.SetSiblingIndex(Overring.RectTransform.GetSiblingIndex() + 1);
                Overring = null;
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (UI.CustomMode && GridLayoutGroup && Moveable)
        {
            Vector2 new_anchor = new Vector2(0.5f, 0.5f);
            UiObj button = RectTransform.GetChild(0).GetComponent<UiObj>();

            if (Dragging.position.x > 1080 / 2)
            {
                new_anchor = new Vector2(1, new_anchor.y);
                button.RectTransform.anchorMin = new Vector2(0, button.RectTransform.anchorMin.y);
                button.RectTransform.anchorMax = new Vector2(0, button.RectTransform.anchorMax.y);
                button.RectTransform.anchoredPosition = new Vector2(0, button.RectTransform.anchoredPosition.y);
                if (OriginSprite && ChangeSprite)
                {
                    button.GetComponent<Image>().sprite = button.OriginSprite;
                    Image.sprite = OriginSprite;
                }
            }
            else
            {
                new_anchor = new Vector2(0, new_anchor.y);
                button.RectTransform.anchorMin = new Vector2(1, button.RectTransform.anchorMin.y);
                button.RectTransform.anchorMax = new Vector2(1, button.RectTransform.anchorMax.y);
                button.RectTransform.anchoredPosition = new Vector2(0, button.RectTransform.anchoredPosition.y);
                if (OriginSprite && ChangeSprite)
                {
                    button.GetComponent<Image>().sprite = button.ChangeSprite;
                    Image.sprite = ChangeSprite;
                }
            }

            button.RectTransform.pivot = new_anchor;
            RectTransform.anchorMin = RectTransform.anchorMax = RectTransform.pivot = new_anchor;
            RectTransform.anchoredPosition = new Vector2(0, RectTransform.anchoredPosition.y);
        }

        if (CMoving != null) StopCoroutine(CMoving);
        CMoving = StartCoroutine(EMoving());

        if (Dragging != null && Dragging.parent == UI.Canvas && Dragging != UI.Menu.RectTransform)
            Dragging.SetSiblingIndex(1);

        Image.raycastTarget = true;
        Dragging = null;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UI.CustomMode && Dragging)
        {
            UiObj drag = Dragging.GetComponent<UiObj>();
            if (drag && !drag.GridLayoutGroup)
            {
                Overring = this;

                if (drag.Moveable && GridLayoutGroup && !Hide && drag.Compositeable)
                {
                    Dragging.SetParent(UI.Menu.RectTransform.GetChild(1).GetComponent<UiObj>().transform);

                    if (CScalingForChild != null) StopCoroutine(CScalingForChild);
                    CScalingForChild = StartCoroutine(EScalingForChild(GetComponent<RectTransform>().childCount));
                }
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (UI.CustomMode && Dragging)
        {
            UiObj drag = Dragging.GetComponent<UiObj>();

            if (drag && !drag.GridLayoutGroup && drag.Moveable && GridLayoutGroup && !Hide && drag.Compositeable)
            {
                Dragging.SetParent(UI.Canvas.transform);
                Dragging.SetSiblingIndex(1);

                if (CScalingForChild != null) StopCoroutine(CScalingForChild);
                CScalingForChild = StartCoroutine(EScalingForChild(GetComponent<RectTransform>().childCount));
            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (UI.CustomMode && Dragging)
        {
            UI.Menu.GridLayoutGroup.enabled = false;
            UI.Menu.GridLayoutGroup.enabled = true;

            if (CMoving != null) StopCoroutine(CMoving);
            CMoving = StartCoroutine(EMoving());
            if (GridLayoutGroup)
            {
                if (CScalingForChild != null) StopCoroutine(CScalingForChild);
                CScalingForChild = StartCoroutine(EScalingForChild(GetComponent<RectTransform>().childCount));
            }
        }
    }
}
