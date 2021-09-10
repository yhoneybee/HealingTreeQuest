using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    [SerializeField] Text plusText;
    [SerializeField] Slider volume;
    [SerializeField] Sprite[] speakers;
    [SerializeField] List<RectTransform> HideButtonParents;

    Coroutine textAnim;

    float saveValue = 0;

    public bool IsUIOpened { get; set; } = false;
    bool isMuted = false;

    void Start()
    {
        volume.value = SoundManager.Instance.TotalVolume;

        foreach (RectTransform rTr in HideButtonParents)
        {
            SoundManager.Instance.SetButtonsSound(rTr.GetComponentsInChildren<Button>());
        }
    }
    public void ActiveOn(GameObject obj)
    {
        if (IsUIOpened)
            return;

        obj.SetActive(true);
        IsUIOpened = true;
    }
    public void ActiveOff(GameObject obj)
    {
        obj.SetActive(false);
        IsUIOpened = false;
    }
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

    public void MuteOnOff()
    {
        isMuted = !isMuted;

        Image image = volume.transform.GetChild(1).GetComponent<Image>();

        if (isMuted)
        {
            saveValue = volume.value;
            volume.value = SoundManager.Instance.TotalVolume = 0;
            image.sprite = speakers[1];
        }

        else
        {
            volume.value = SoundManager.Instance.TotalVolume = saveValue;
            image.sprite = speakers[0];
        }
    }
    public void SetVolume()
    {
        if (isMuted)
        {
            volume.value = 0;
            return;
        }
        volume.transform.GetChild(1).GetComponent<Image>().sprite = speakers[0];
        SoundManager.Instance.TotalVolume = volume.value;
    }


    public void TextAnim(string text)
    {
        if (textAnim != null) StopCoroutine(textAnim);
        textAnim = StartCoroutine(_TextAnim(text));
    }
    IEnumerator _TextAnim(string text)
    {
        plusText.gameObject.SetActive(true);
        plusText.color = text.StartsWith("+") ? Color.blue : Color.red;
        plusText.color = plusText.color + new Color(0, 0, 0, 1);
        plusText.text = text;
        plusText.rectTransform.localScale = new Vector2(0.1f, 0.1f);

        while (true)
        {
            if (plusText.rectTransform.localScale.x >= 0.9f)
            {
                plusText.rectTransform.localScale = new Vector2(1, 1);
                break;
            }
            plusText.rectTransform.localScale = Vector2.Lerp(plusText.rectTransform.localScale, Vector2.one, 0.2f);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1);
        while (true)
        {
            if (plusText.color.a <= 0) break;
            yield return new WaitForSeconds(0.01f);
            plusText.color = new Color(plusText.color.r, plusText.color.g, plusText.color.b, plusText.color.a - 0.005f);
        }

        plusText.gameObject.SetActive(false);
        plusText.color = plusText.color + new Color(0, 0, 0, 1);
    }
}
