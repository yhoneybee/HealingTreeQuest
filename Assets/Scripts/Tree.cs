using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    [Header("나무 관련 사항")]
    public Mesh[] Trees;

    private MeshFilter meshFilter;

    public int Level;
    public int Exp;
    [SerializeField] int MaxExp;

    [Header("레벨 관련 사항")]
    [SerializeField] Slider levelSlider;
    [SerializeField] Text levelText;
    [SerializeField] Image tree_Level_Image;
    [Space(10)]
    [SerializeField] List<Sprite> level_Sprites = new List<Sprite>();

    LeafUpgrade leaf;

    [Header("파티클 관련 사항")]
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
        levelSlider.value = (float)Exp / MaxExp;
        if (Exp >= MaxExp)
        {
            Level++;
            particle_Scale += 0.01f;

            GameObject obj = Instantiate(levelUp_Particle, gameObject.transform);
            obj.transform.localScale = particle_Scale * new Vector3(1, 1, 1);

            Exp -= MaxExp;
            levelText.text = Level.ToString();

            if (Level > 30 && Level < 61)
                MaxExp = 15000;
            else if (Level > 60 && Level < 100)
                MaxExp = 30000;
            else
                MaxExp = 10000;

            StartCoroutine(TreeAnimation());

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

    public IEnumerator TreeAnimation()
    {
        Vector3 targetScale = transform.localScale + new Vector3(20, 20, 20);
        while (true)
        {
            if (Vector3.Distance(transform.localScale, targetScale) <= 1f)
            {
                transform.localScale = targetScale;

                int MeshCount = Level % 10;

                if (MeshCount == 0)
                {
                    meshFilter.sharedMesh = Trees[(Level / 10) - 1];
                    leaf.leafUpgrade(Level);
                }
                yield break;
            }
            yield return new WaitForSeconds(0.01f);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 4);
        }
    }
}
