using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    Camera camera;
    Rect rect;
    float scaleHeight;
    float scaleWidth;
    void Start()
    {
        camera = GetComponent<Camera>();
    }
    void Update()
    {
        rect = camera.rect;
        scaleHeight = ((float)Screen.width / Screen.height) / ((float)9 / 16);
        scaleWidth = 1f / scaleHeight;
        if (scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }
        camera.rect = rect;
    }
}
