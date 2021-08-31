using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSelect : MonoBehaviour
{
    public void StartMiniGame(string Game_name)
    {
        SceneManager.LoadScene(Game_name);
    }
}
