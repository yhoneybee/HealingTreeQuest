using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiObj : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    Image Image;
    Coroutine c_preview = null;
    public static UiObj DraggingUi;

    bool preview = false;
    public bool compositable = false;

    public bool Preview
    {
        get
        {
            return preview;
        }
        set
        {
            preview = value;
            if (c_preview != null) StopCoroutine(c_preview);
            c_preview = StartCoroutine(CPreview());
        }
    }

    IEnumerator CPreview()
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

    private void Start()
    {
        Image = GetComponent<Image>();
    }

    // Ui를 드레그 중일 때 ( UiManager에 있는 UiCustomMode가 true일 때만 가능하다 )
    public void OnDrag(PointerEventData eventData)
    {
        if (UiManager.Instance.UiCustomMode && DraggingUi)
        {
            DraggingUi.transform.position = new Vector3(UiManager.Instance.MousePos.x * 10, UiManager.Instance.MousePos.y * 10, DraggingUi.transform.position.z);
        }
    }

    // Ui 모음집에 Drop했을 때에 
    public void OnDrop(PointerEventData eventData)
    {
        if (DraggingUi)
            DraggingUi.transform.SetParent(transform);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DraggingUi = this;
        if (compositable) GetComponent<Image>().raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggingUi = null;
        if (compositable) GetComponent<Image>().raycastTarget = true;
    }
}
