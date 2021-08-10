using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Weeds : MonoBehaviour
{
    public bool isSpawned = false;
    public Slider slider;

    private void Start()
    {
        GetComponent<Image>().enabled = false;
    }

    public void Init(GameObject obj)
    {
        Slider tempSlider = Resources.Load<Slider>("Prefabs/Minigame/���� �̱�/Guage");
        slider = Instantiate(tempSlider, obj.transform);
        slider.transform.localPosition = new Vector2(0, 150);
        isSpawned = true;
        GetComponent<Image>().enabled = true;
    }

    public void Release()
    {
        if (!isSpawned)
        {
            Debug.Log("���� �ָ� ����� ���� ����");
            return;
        }

        isSpawned = false;
        Destroy(transform.GetChild(0).gameObject);
        GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        if (isSpawned)
            slider.value += Time.deltaTime;
    }
}
