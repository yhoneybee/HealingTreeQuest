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
        ClearNumber = DonDestroyOnLoadObj.instance.ClearList[Number];

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
        DonDestroyOnLoadObj.instance.SelectMiniGame = Number;
        SceneManager.LoadScene(GameName.text);
    }

    public void GiveGift()
    {
        if (DonDestroyOnLoadObj.instance.ClearList[Number] == 1)
        {
            GiveGiftObj.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            GameObject.Find("Tree").GetComponent<Tree>().Exp += 3000;
            DonDestroyOnLoadObj.instance.ClearList[Number] = 2;
        }
    }
}
