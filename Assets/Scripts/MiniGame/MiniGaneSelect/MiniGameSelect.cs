using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSelect : MonoBehaviour
{
    public int Number;
    public void StartMiniGame(string Game_name)
    {
        DDOLObj.Instance.SelectMiniGame = Number;
        SceneManager.LoadScene(Game_name);
    }
}
