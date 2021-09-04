using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    public void ReTry()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public void GoHome()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ingame");
    }

    public void SetVolume(Slider slider)
    {
        SoundManager.Instance.Volume = slider.value;
    }
}
