using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnClickMethods : MonoBehaviour
{
    public UiManager UI => UiManager.Instance;

    public void SwitchCustomMode()
    {
        UI.CustomMode = !UI.CustomMode;
    }

    public void PreviewMode()
    {
        foreach (var item in UI.UiObjs)
        {
            item.Preview = !item.Preview;
        }
    }

    public void CustomMode()
    {
        UI.CustomMode = !UI.CustomMode;
    }
}
