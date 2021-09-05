using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniGameProp : MonoBehaviour
{
    public Text GameName;
    public int Number;
    public int ClearNumber;
    public GameObject GoMiniGameObj;
    public GameObject GiveGiftObj;

    void Start()
    {
        ClearNumber = DDOLObj.Instance.ClearList[Number];

        if (ClearNumber == 1)
        {
            GoMiniGameObj.SetActive(false);
            GiveGiftObj.SetActive(true);
            transform.SetSiblingIndex(0);
        }
        else if(ClearNumber==2)
        {
            GoMiniGameObj.SetActive(false);
            GiveGiftObj.SetActive(true);
            GiveGiftObj.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    public void GoMiniGame()
    {
        DDOLObj.Instance.SelectMiniGame = Number;
        SceneManager.LoadScene(GameName.text);
    }

    public void GiveGift()
    {
        if (DDOLObj.Instance.ClearList[Number] == 1)
        {
            GiveGiftObj.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
            GameObject.Find("Tree").GetComponent<Tree>().Exp += 3000;
            UiManager.Instance.TextAnim(3000);
            DDOLObj.Instance.ClearList[Number] = 2;
        }
    }
}
