using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    public Mesh[] Trees;

    private MeshFilter meshFilter;

    public int Level;
    public int Exp;
    [SerializeField] int MaxExp;

    [SerializeField] Text LevelText;
    [SerializeField] List<Sprite> level_Sprites = new List<Sprite>();
    [SerializeField] Image tree_Level_Image;
    LeafUpgrade leaf;
    [SerializeField] float particle_Scale;
    [SerializeField] GameObject levelUp_Particle;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        leaf = FindObjectOfType<LeafUpgrade>();
    }

    void Update()
    {
        //meshFilter.sharedMesh = Trees[0];
        if (Exp >= MaxExp)
        {
            Level++;
            particle_Scale += 0.01f;

            GameObject obj = Instantiate(levelUp_Particle, gameObject.transform);
            obj.transform.localScale = particle_Scale * new Vector3(1, 1, 1);

            Exp -= MaxExp;
            LevelText.text = Level.ToString();

            if (Level > 30 && Level < 61)
                MaxExp = 15000;
            else if (Level > 60 && Level < 100)
                MaxExp = 30000;
            else
                MaxExp = 10000;

            int MeshCount = Level % 10;

            if (MeshCount == 0)
            {
                meshFilter.sharedMesh = Trees[(Level / 10) - 1];
                leaf.leafUpgrade(Level);
            }


            if (Level >= 40)
                tree_Level_Image.sprite = level_Sprites[3];
            else if (Level >= 30)
                tree_Level_Image.sprite = level_Sprites[2];
            else if (Level >= 20)
                tree_Level_Image.sprite = level_Sprites[1];
            else if (Level >= 10)
                tree_Level_Image.sprite = level_Sprites[0];
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Exp += 3000;
        }

    }
}
