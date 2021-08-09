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

    // Ui�� �巹�� ���� �� ( UiManager�� �ִ� UiCustomMode�� true�� ���� �����ϴ� )
    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Ui �������� Drop���� ���� 
    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // �ʿ� �������� ��
    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // �ʿ� �������� ��
    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
