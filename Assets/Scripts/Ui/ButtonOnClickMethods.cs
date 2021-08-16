using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnClickMethods : MonoBehaviour
{
    public UiManager UI => UiManager.Instance;

    public void SwitchHideMode()
    {
        UI.Menu.Hide = !UI.Menu.Hide;
    }

    public void SwitchPreviewMode()
    {
        foreach (var item in UI.UiObjs)
        {
            item.Preview = !item.Preview;
        }
    }

    public void SwitchCustomMode()
    {
        UI.CustomMode = !UI.CustomMode;
    }
}
