using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; } = null;

    public readonly List<UiObj> ui_objs = new List<UiObj>();
    public bool UiCustomMode { get; set; } = false;

    public Vector2 MousePos { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ui_objs.AddRange(FindObjectsOfType<UiObj>());
    }

    private void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }
}
