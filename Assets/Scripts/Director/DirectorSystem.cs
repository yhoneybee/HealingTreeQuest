using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorSystem : MonoBehaviour
{
    [HideInInspector]
    public CameraSystem cameraSystem;
    public VisualSystem visualSystem;
    void Start()
    {
        cameraSystem = GetComponent<CameraSystem>();
        visualSystem = GetComponent<VisualSystem>();
    }

    void Update()
    {

    }
}
