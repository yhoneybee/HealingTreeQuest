using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weeds : MonoBehaviour
{
    public bool isSpawned = false;
    public Slider slider;

    public void Init(GameObject obj)
    {
        Slider tempSlider = Resources.Load<Slider>("Prefabs/Minigame/¿‚√  ªÃ±‚/Guage");
        slider = Instantiate(tempSlider, obj.transform);
        slider.transform.localPosition = new Vector2(0, 150);
        isSpawned = true;
    }

    public void Release()
    {
        isSpawned = false;
        Destroy(transform.GetChild(0).gameObject);
        GetComponent<Image>().sprite = null;
    }

    void Update()
    {
        if (isSpawned)
            slider.value += Time.deltaTime;
    }
}
