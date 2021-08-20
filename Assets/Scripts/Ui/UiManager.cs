using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; } = null;

    public List<UiObj> UiObjs = new List<UiObj>();
    public RectTransform Canvas;
    public UiObj Menu;

    Vector2 Now;
    Vector2 Prev;

    float force = 0;

    private bool custom_mode = false;
    public bool CustomMode
    {
        get { return custom_mode; }
        set { custom_mode = value; }
    }

    public bool Preview = false;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Preview)
        {
            if (Input.GetMouseButtonDown(0))
                Now = Prev = Input.mousePosition;
            if (Input.GetMouseButton(0))
                Now = Input.mousePosition;

            Camera.main.transform.RotateAround(new Vector3(0, 4.5f, 0), Vector3.up, (Now.x - Prev.x) * Time.deltaTime * 10);
        }
    }
    private void LateUpdate()
    {
        if (Preview)
        {
            if (Input.GetMouseButton(0))
            {
                Prev = Now;
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
}
