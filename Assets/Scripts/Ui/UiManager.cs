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
}
