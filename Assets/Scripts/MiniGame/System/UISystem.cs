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
            Debug.LogError("오브젝트가 없음!");
            return null;
        }

        Text text = textObj.GetComponent<Text>();
        if(!text)
        {
            Debug.LogError("컴포넌트가 없음!");
            return null;
        }

        return text;
    }
}
