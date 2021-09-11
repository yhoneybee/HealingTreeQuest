using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    [Header("나무 관련 사항")]
    public Mesh[] Trees;
    public Material[] materials = new Material[2];
    public Material[] materials2 = new Material[2];

    private MeshFilter meshFilter;
    private MeshRenderer Mrend;

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
    public float particle_Scale;
    [SerializeField] GameObject levelUp_Particle;
    [SerializeField] GameObject MeshChange_Particle;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        Mrend = GetComponent<MeshRenderer>();

        leaf = FindObjectOfType<LeafUpgrade>();
    }

    void Update()
    {
        //meshFilter.sharedMesh = Trees[0];
        levelSlider.value = Mathf.Lerp(levelSlider.value, (float)Exp / MaxExp, Time.deltaTime * 3);
        if (Level >= 100) return;
        if (Exp >= MaxExp)
        {
            Level++;
            SoundManager.Instance.Play("Grow", SoundType.EFFECT);
            particle_Scale += 0.1f;

            int MeshCount = Level % 10;

            if (MeshCount != 0)
            {
                GameObject obj = Instantiate(levelUp_Particle, gameObject.transform);
                obj.transform.localScale = particle_Scale * new Vector3(1, 1, 1);
            }
            else
            {
                GameObject obj = Instantiate(MeshChange_Particle, gameObject.transform);
                obj.transform.position = Vector3.zero;
                obj.transform.localScale = particle_Scale / 2 * new Vector3(1, 1, 1);
                obj.GetComponent<TreeUpgrade_Particle>().notMove = true;
            }


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

        if (Input.GetKey(KeyCode.Space))
        {
            Exp += 10000;
            UiManager.Instance.TextAnim(10000);
        }
    }

    public void LevelUp()
    {
        Exp += MaxExp;
    }

    public IEnumerator TreeAnimation()
    {
        Vector3 targetScale = transform.localScale + new Vector3(0.5f, 0.5f, 0.5f); // 목표 크기 설정
        while (true)
        {
            if (Vector3.Distance(transform.localScale, targetScale) <= 1f)
            {
                transform.localScale = targetScale;

                int MeshCount = Level % 10;

                if (MeshCount == 0)
                {
                    switch (Level)
                    {
                        case 10:
                            transform.localScale = new Vector3(40, 40, 40);
                            break;
                        case 40:
                            transform.localScale = new Vector3(90, 90, 90);
                            break;
                        case 60:
                            transform.localScale = new Vector3(350, 350, 350);
                            break;
                        case 70:
                            transform.localScale = new Vector3(100, 100, 100);
                            break;
                        case 80:
                            transform.localScale = new Vector3(900, 900, 900);
                            break;
                        case 90:
                            transform.localScale = new Vector3(400, 400, 400);
                            break;
                    }

                    try
                    {
                        meshFilter.sharedMesh = Trees[(Level / 10) - 1];
                    }
                    catch (System.IndexOutOfRangeException e)
                    {
                        UiManager.Instance.TypeingEffect();
                        Debug.Log("최대 외형 도달!");
                    }

                    if (Level == 40 || Level == 60 || Level == 80)
                        Mrend.materials = materials2;
                    else
                        Mrend.materials = materials;

                    if (Level == 40)
                        transform.rotation = Quaternion.Euler(-30, 180, 90);
                    else if (Level == 80)
                        transform.rotation = Quaternion.Euler(-120, -40, 35);
                    else
                        transform.rotation = Quaternion.Euler(-90, 0, 0);
                }
                yield break;
            }
            yield return new WaitForSeconds(0.01f);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 4);
        }
    }
}
