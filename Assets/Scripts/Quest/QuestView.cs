using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    public Quest Quest { get; private set; } = null;

    private void Start()
    {
        if (Quest != null)
            GetComponent<Image>().sprite = Quest.QuestData.QuestIcon;
    }
}
