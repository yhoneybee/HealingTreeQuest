using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class isPannel_Open : MonoBehaviour
{
    [SerializeField] Button[] buttons = new Button[2];

    public void enable(bool isenable)
    {
        foreach (Button item in buttons)
        {
            item.enabled = isenable;
        }
    }
}
