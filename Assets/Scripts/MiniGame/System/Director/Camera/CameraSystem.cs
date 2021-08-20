using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    Camera Camera => Camera.main;

    void Start()
    {
    }

    void Update()
    {

    }

    public void ShakeCam(float power, float time)
    {
        StartCoroutine(_ShakeCam(power, time));
    }

    private IEnumerator _ShakeCam(float power, float time)
    {
        float curTime = Time.time, shakePower = power * (Camera.orthographicSize / 30);
        Vector2 randomPos;

        while (Time.time - curTime < time)
        {
            randomPos = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2)) * shakePower;
            Camera.transform.Translate(randomPos);
            yield return new WaitForSeconds(0.01f);
            Camera.transform.Translate(-randomPos);
        }
    }
}
