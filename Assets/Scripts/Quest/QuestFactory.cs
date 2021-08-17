using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestFactory : MonoBehaviour
{
    public List<QuestData> QuestDatas = new List<QuestData>();

    public QuestData GetRandomQuest() => QuestDatas[Random.Range(0, QuestDatas.Count)];
}
