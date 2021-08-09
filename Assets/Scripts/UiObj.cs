using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiObj : MonoBehaviour, IDragHandler, IDropHandler, IPointerDownHandler, IPointerUpHandler
{
    bool preview = false;
    public bool Preview
    {
        get
        {
            return preview;
        }
        set
        {
            preview = value;
            gameObject.SetActive(!preview);
        }
    }

    // Ui를 드레그 중일 때 ( UiManager에 있는 UiCustomMode가 true일 때만 가능하다 )
    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Ui 모음집에 Drop했을 때에 
    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // 필요 없을지도 모름
    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // 필요 없을지도 모름
    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
