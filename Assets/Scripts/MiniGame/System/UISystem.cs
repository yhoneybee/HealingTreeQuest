using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    public Text GetText(string name)
    {
        GameObject textObj = GameObject.Find(name);
        if (!textObj)
        {
            Debug.LogError("������Ʈ�� ����!");
            return null;
        }

        Text text = textObj.GetComponent<Text>();
        if(!text)
        {
            Debug.LogError("������Ʈ�� ����!");
            return null;
        }

        return text;
    }
}
