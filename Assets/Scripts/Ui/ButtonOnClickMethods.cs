using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClickMethods : MonoBehaviour
{
    public UiManager UI => UiManager.Instance;

    IEnumerator PannelOpenCoru;

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
        UI.Preview = !UI.Preview;
    }

    public void SwitchCustomMode()
    {
        StopAllCoroutines();
        foreach (var item in UI.UiObjs)
        {
            var button = item.GetComponent<Button>();
            if (button && button.name != "Custom") button.enabled = UI.CustomMode;
        }
        UI.PreviewButton.enabled = UI.CustomMode;
        UI.CustomMode = !UI.CustomMode;
        PannelOpenCoru = CustomPannel(UI.CustomMode);
        StartCoroutine(PannelOpenCoru);
    }

    IEnumerator CustomPannel(bool Open)
    {

        Image Pannel = GameObject.Find("CustomPannel").GetComponent<Image>();
        Text txt = GameObject.Find("CustomText").GetComponent<Text>();
        while (true)
        {
            //0.5f
            Pannel.color += Open ? new Color(0, 0, 0, 0.025f) : new Color(0, 0, 0, -0.025f);
            txt.color += Open ? new Color(0, 0, 0, 0.05f) : new Color(0, 0, 0, -0.05f);

            if (Pannel.color.a >= 0.5f && Open)
                break;
            else if (Pannel.color.a <= 0 && !Open)
                break;

            yield return new WaitForSeconds(0.05f);
        }

    }
}
