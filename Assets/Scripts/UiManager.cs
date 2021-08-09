using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; } = null;

    public readonly List<UiObj> ui_objs = new List<UiObj>();
    public bool UiCustomMode { get; set; } = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ui_objs.AddRange(GetComponents<UiObj>());
    }

    public void Preview()
    {
        foreach (var ui in ui_objs)
        {
            ui.Preview = !ui.Preview;
        }
    }
}
