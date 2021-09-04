using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeUpgrade_Particle : MonoBehaviour
{
    public Vector3 Speed;
    [SerializeField] float upSpeed;
    public bool notMove;

    Tree tree;
    void Start()
    {
        tree = GameObject.Find("Tree").GetComponent<Tree>();
        if (!notMove)
            upSpeed += tree.particle_Scale;
    }

    void Update()
    {
        transform.Rotate(Speed * Time.deltaTime);

        transform.position += Vector3.up * Time.deltaTime * upSpeed;

        if (gameObject.GetComponent<ParticleSystem>().isStopped == true)
        {

            //StartCoroutine(tree.TreeAnimation());
            Destroy(gameObject);
        }
    }
}
