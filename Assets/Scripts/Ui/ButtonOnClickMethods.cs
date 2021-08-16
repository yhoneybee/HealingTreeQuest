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
}
