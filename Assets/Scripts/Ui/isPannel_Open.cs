using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class isPannel_Open : MonoBehaviour
{
    public UiManager UI => UiManager.Instance;
    public void enable(bool isenable)
    {
        foreach (var item in UI.UiObjs)
        {
            var button = item.GetComponent<Button>();
            if (button) button.enabled = isenable;
        }
        UI.PreviewButton.enabled = isenable;
    }

    public void UIClickSound() => SoundManager.Instance.Play("BtnClick");
}
