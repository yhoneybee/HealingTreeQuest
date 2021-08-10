using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn : MonoBehaviour
{
    public void Preview()
    {
        foreach (var ui in UiManager.Instance.ui_objs)
        {
            ui.Preview = !ui.Preview;
        }
    }

    public void CustomMode() => UiManager.Instance.UiCustomMode = !UiManager.Instance.UiCustomMode;
}
