using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static RectTransform Dragging = null;

    public UiManager UI => UiManager.Instance;

    public Image Image = null;
    public Coroutine CSwitchingHide = null;
    public Coroutine CPreview = null;
    public Coroutine CScalingForChild = null;
    public RectTransform RectTransform = null;
    public GridLayoutGroup GridLayoutGroup = null;

    public Vector2 MousePos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

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

    float cell_size = 0, spacing = 0, padding = 0;

    void Start()
    {
        if (GridLayoutGroup)
        {
            cell_size = GridLayoutGroup.cellSize.x;
            spacing = GridLayoutGroup.spacing.y;
            padding = 150;
            RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, (cell_size + spacing) + padding);
        }
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
        Dragging = RectTransform;
        BeginMousePos = MousePos;
        UiPos = Dragging.position;
        Image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = MousePos - BeginMousePos;

        delta += UiPos;

        Dragging.position = new Vector3(delta.x, delta.y, Dragging.position.z);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 new_anchor = new Vector2(0.5f, 0.5f);

        if (Dragging.position.x > 0)
        {
            new_anchor = new Vector2(1, new_anchor.y);
            GridLayoutGroup.childAlignment = TextAnchor.MiddleRight;
            RectTransform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            RectTransform.pivot = new_anchor;
        }
        else
        {
            new_anchor = new Vector2(0, new_anchor.y);
            GridLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
            RectTransform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
            RectTransform.pivot = new Vector2(1, new_anchor.y);
        }

        RectTransform.anchorMin = RectTransform.anchorMax = new_anchor;
        RectTransform.anchoredPosition = new Vector2(0, RectTransform.anchoredPosition.y);

        Image.raycastTarget = true;
        Dragging = null;
    }
}
