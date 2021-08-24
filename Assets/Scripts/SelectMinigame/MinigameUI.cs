using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameUI : MonoBehaviour
{
    [SerializeField] int GiftNumber;
    [SerializeField] List<GameObject> GiftObj = new List<GameObject>();

    public void AllGift()
    {
        for (int i = 0; i < GiftNumber; ++i)
        {
            if (DDOLObj.Instance.ClearList[i] == 1)
            {
                GiftObj[i].GetComponent<MiniGameProp>().GiveGiftObj.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
                GameObject.Find("Tree").GetComponent<Tree>().Exp += 3000;
                DDOLObj.Instance.ClearList[i] = 2;
            }
        }
    }
}
