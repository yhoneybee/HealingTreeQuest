using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    Enemy enemyPrefab;
    Queue<Enemy> enemys = new Queue<Enemy>();
    private void Start()
    {
        enemyPrefab = Resources.Load<Enemy>("Prefabs/MiniGame/해충 디펜스/Enemy");

        Enemy enemy;
        for (int i = 0; i < 5; i++)
        {
            enemy = Instantiate(enemyPrefab, transform);
            enemy.gameObject.SetActive(false);
            enemys.Enqueue(enemy);
        }
    }
    public Enemy GetEnemy()
    {
        Enemy enemy;

        if (enemys.Count <= 0)
            enemy = Instantiate(enemyPrefab, transform);
        else
            enemy = enemys.Dequeue();

        enemy.gameObject.SetActive(true);
        return enemy;
    }
    public void ReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemys.Enqueue(enemy);
    }
}