using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    private Queue<Enemy> enemyQueue;

    private void Start()
    {
        enemyQueue = new Queue<Enemy>();
        AddEnemy(10);
    }

    public Enemy Get()
    {
        if (enemyQueue.Count == 0)
        {
            AddEnemy(1);
        }

        return enemyQueue.Dequeue();
    }

    private void AddEnemy(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Enemy b = Instantiate(enemyPrefab, transform);
            b.gameObject.SetActive(false);
            enemyQueue.Enqueue(b);
        }
    }

    public void ReturnToPool(Enemy b)
    {
        enemyQueue.Enqueue(b);
    }
}
