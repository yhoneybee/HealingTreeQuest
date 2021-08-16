using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem4 : MonoBehaviour
{
    public Transform[] spawnPoints;
    List<GameObject> trashes = new List<GameObject>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        for(int i = 0; i < 3; i++)
        {
            if(spawnPoints[i].GetChild(0))
            {
                Instantiate(trashes[Random.Range(0, trashes.Count)], spawnPoints[i].transform);
            }
        }
        yield return null;
    }
}
