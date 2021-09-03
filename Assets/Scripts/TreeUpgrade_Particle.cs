using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeUpgrade_Particle : MonoBehaviour
{
    public Vector3 Speed;
    [SerializeField] float upSpeed;
    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(Speed * Time.deltaTime);

        transform.position += Vector3.up * Time.deltaTime * upSpeed;

        if (gameObject.GetComponent<ParticleSystem>().isStopped == true)
        {
            Tree tree = GameObject.Find("Tree").GetComponent<Tree>();

            //StartCoroutine(tree.TreeAnimation());
            Destroy(gameObject);
        }
    }
}
