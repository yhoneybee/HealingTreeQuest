using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; } = null;

    public Camera Cam => Camera.main;
    public Transform CamTf => Cam.transform;

    public List<UiObj> UiObjs = new List<UiObj>();
    public RectTransform Canvas;
    public UiObj Menu;

    public Vector3 MouseCenterPos => Cam.ScreenToViewportPoint(Input.mousePosition);

    public Vector3 WoodPos = new Vector3(0, 4.5f, 0);

    Vector2 Now;
    Vector2 Prev;

    public float Distance = 10;
    float zoom = 60;

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
            if (Input.GetMouseButtonDown(0)) Prev = MouseCenterPos;
            else if (Input.GetMouseButton(0))
            {
                Now = MouseCenterPos;
                Vector3 dir = Prev - Now;

                Vector2 xy_axis = new Vector2(-dir.x * 100, dir.y * 100);

                CamTf.transform.position = WoodPos;

                CamTf.Rotate(Vector3.up, xy_axis.x, Space.World);
                CamTf.Rotate(Vector3.right, xy_axis.y);

                if (CamTf.localEulerAngles.x > 300)
                    CamTf.localEulerAngles = new Vector3(0, CamTf.localEulerAngles.y, CamTf.localEulerAngles.z);
                else if (CamTf.localEulerAngles.x > 30)
                    CamTf.localEulerAngles = new Vector3(30, CamTf.localEulerAngles.y, CamTf.localEulerAngles.z);

                CamTf.Translate(new Vector3(0, 0, -Distance));

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
