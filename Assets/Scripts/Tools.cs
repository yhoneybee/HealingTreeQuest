using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools<T> : MonoBehaviour
{
    public static T GetTool(string name)
    {
        GameObject obj = GameObject.Find(name);

        T t = obj.GetComponent<T>();

        return t;
    }
}