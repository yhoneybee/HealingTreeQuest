using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest
{
    public Quest(QuestData questData) => QuestData = questData;
    public QuestData QuestData;

    public bool Cleared = false;
}
