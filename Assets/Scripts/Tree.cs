using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Mesh[] Trees;

    private MeshFilter meshFilter;

    public int Level;
    public int Exp;
    private int MaxExp;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    void Update()
    {
        //meshFilter.sharedMesh = Trees[0];
        if(Exp>=MaxExp)
        {
            Level++;
            Exp -= MaxExp;
            if (Level > 30 && Level < 61)
                MaxExp = 15000;
            else if (Level > 60 && Level < 100)
                MaxExp = 30000;
            else
                MaxExp = 10000;

            int MeshCount = Level % 10;

            if(MeshCount==0)
            {
                meshFilter.sharedMesh = Trees[(Level / 10) - 1];
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Exp += 3000;
        }
    }
}
