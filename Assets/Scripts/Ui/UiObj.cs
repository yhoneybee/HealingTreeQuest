using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public static UiObj Dragging = null;

    public UiManager UI => UiManager.Instance;

    public Image Image = null;
    public RectTransform RectTransform = null;
    public GridLayoutGroup GridLayoutGroup = null;

    public Vector2 MousePos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public bool Compositable = false;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public Vector2 PivotLerpChange(Vector2 newPivot) => Vector2.Lerp(RectTransform.pivot, newPivot, Time.deltaTime* 10);

    public void OnBeginDrag(PointerEventData eventData)
    {
        Dragging = this;
        Image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Dragging)
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
                    newAnchor = new Vector2(1, newAnchor.y);
                else if (RectTransform.position.x < 0) // Left
                    newAnchor = new Vector2(0, newAnchor.y);

                RectTransform.anchorMin = RectTransform.anchorMax = newAnchor;
                RectTransform.pivot = PivotLerpChange(newAnchor);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
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

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Compositable)
        {
            RectTransform.pivot = RectTransform.anchorMin;
            RectTransform.anchoredPosition = new Vector2(0, Dragging.RectTransform.anchoredPosition.y);
        }

        Image.raycastTarget = true;
        Dragging = null;
    }
}
