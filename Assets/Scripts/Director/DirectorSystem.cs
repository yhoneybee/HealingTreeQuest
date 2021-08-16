using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorSystem : MonoBehaviour
{
    [HideInInspector]
    public CameraSystem cameraSystem;
    void Start()
    {
        cameraSystem = GetComponent<CameraSystem>();
    }

    void Update()
    {

    }
}
