using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    public static UiObj Dragging = null;

    public UiManager UI => UiManager.Instance;

    public Image Image = null;
    public Coroutine CMove = null;
    public Coroutine CPreview = null;
    public RectTransform RectTransform = null;
    public GridLayoutGroup GridLayoutGroup = null;

    public Vector2 MousePos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private bool preview = false;
    public bool Preview
    {
        get
        {
            return preview;
        }
        set
        {
            preview = value;
            if (CPreview != null) StopCoroutine(CPreview);
            CPreview = StartCoroutine(EPreview());
        }
    }

    private bool hide = false;
    public bool Hide
    {
        get { return hide; }
        set
        {
            hide = value;
            ChildPreview = value;
        }
    }

    private bool child_preview;

    public bool ChildPreview
    {
        get { return child_preview; }
        set 
        { 
            child_preview = value;
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).GetComponent<UiObj>().Preview = value;
        }
    }


    public bool Compositable = false;
    public bool Moveable = true;


    private void Start()
    {
    }

    private void Update()
    {
    }

    public Vector2 PivotLerpChange(Vector2 newPivot) => Vector2.Lerp(RectTransform.pivot, newPivot, Time.deltaTime * 10);

    IEnumerator EMove()
    {
        if (Hide)
        {
            // 0.5가 가운데 기준임 그래서 0을 기준으로 함 Mathf.Abs(UI.MiddlePivot - 0.5f) + 0.5f
            while (RectTransform.pivot.x < UI.MiddlePivot - 0.01f || RectTransform.pivot.x > UI.MiddlePivot + 0.01f)
            {
                RectTransform.pivot = PivotLerpChange(new Vector2(UI.MiddlePivot, RectTransform.pivot.y));
                yield return new WaitForSeconds(0.005f);
            }
            RectTransform.pivot = new Vector2(UI.MiddlePivot, RectTransform.pivot.y);
        }
        else
        {
            while (RectTransform.pivot.x > 0.1f || RectTransform.pivot.x < 0.9f)
            {
                if (1 - RectTransform.pivot.x <= 0.01f || RectTransform.pivot.x <= 0.01f)
                    break;
                RectTransform.pivot = PivotLerpChange(new Vector2(RectTransform.anchorMin.x, RectTransform.pivot.y));
                yield return new WaitForSeconds(0.005f);
            }
            RectTransform.pivot = new Vector2(RectTransform.anchorMin.x, RectTransform.pivot.y);
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (UI.CustomMode && Moveable)
        {
            Dragging = this;
            Image.raycastTarget = false;
            Hide = false;
            ChildPreview = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Dragging && Moveable)
        {
            RectTransform.position = new Vector3(MousePos.x, MousePos.y, RectTransform.position.z);

            if (Compositable)
            {
                Vector2 newAnchor = new Vector2();

                if (RectTransform.position.y > 0.75) // Up
                    newAnchor = new Vector2(RectTransform.pivot.x, 1);
                else if (RectTransform.position.y < -0.75) // Down
                    newAnchor = new Vector2(RectTransform.pivot.x, 0);
                else
                    newAnchor = new Vector2(RectTransform.pivot.x, 0.5f);

                if (RectTransform.position.x > 0) // Right
                {
                    newAnchor = new Vector2(1, newAnchor.y);
                    GridLayoutGroup.childAlignment = TextAnchor.MiddleRight;
                }
                else if (RectTransform.position.x < 0) // Left
                {
                    newAnchor = new Vector2(0, newAnchor.y);
                    GridLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
                }

                RectTransform.anchorMin = RectTransform.anchorMax = newAnchor;
                RectTransform.pivot = PivotLerpChange(newAnchor);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Dragging)
        {
            if (Compositable)
            {
                Dragging.RectTransform.SetParent(transform);
                GridLayoutGroup.enabled = false;
                GridLayoutGroup.enabled = true;
            }
            else
            {
                Dragging.RectTransform.SetParent(UI.Canvas.transform);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Dragging)
        {
            if (Compositable)
            {
                RectTransform.pivot = RectTransform.anchorMin;
                RectTransform.anchoredPosition = new Vector2(0, Dragging.RectTransform.anchoredPosition.y);
            }

            ChildPreview = false;
            Image.raycastTarget = true;
            Dragging = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Compositable)
        {
            Hide = !Hide;
            if (CMove != null)
                StopCoroutine(CMove);

            CMove = StartCoroutine(EMove());
        }
    }
}
