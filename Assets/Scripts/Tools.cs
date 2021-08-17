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

    public static T GetResource(string name)
    {
        GameObject obj = Resources.Load<GameObject>(name);

        T t = obj.GetComponent<T>();

        return t;
    }
    public static T[] GetResourceAll(string name)
    {
        GameObject[] objs = Resources.LoadAll<GameObject>(name);

        T[] ts = new T[objs.Length];
        for(int i = 0; i < objs.Length; i++)
        {
            ts[i] = objs[i].GetComponent<T>();
        }

        return ts;
    }
}