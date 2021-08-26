using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafUpgrade : MonoBehaviour
{
    public Mesh[] leafs;

    private MeshFilter meshFilter;
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    void Update()
    {

    }

    public void leafUpgrade(int Level)
    {
        meshFilter.sharedMesh = leafs[(Level / 10) - 1];
    }
}
