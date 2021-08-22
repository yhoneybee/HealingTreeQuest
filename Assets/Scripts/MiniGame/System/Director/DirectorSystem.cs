using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorSystem : MonoBehaviour
{
    [HideInInspector]
    public CameraSystem cameraSystem;
    public VisualSystem visualSystem;
    public delegate void Func();
    void Awake()
    {
        cameraSystem = GetComponent<CameraSystem>();
        visualSystem = GetComponent<VisualSystem>();
    }

    void Update()
    {

    }
}
