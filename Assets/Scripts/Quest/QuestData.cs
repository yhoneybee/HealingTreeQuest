using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Datas/Quest", order = 1)]
public class QuestData : ScriptableObject
{
    public string Name = "";
    public int id = -1;
    public Sprite QuestIcon = null;
    public string SubHeading = "";
    public float Result = 0;

    public string QuestSceneName = "";
}
