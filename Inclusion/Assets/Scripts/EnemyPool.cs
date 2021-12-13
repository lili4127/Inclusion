using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy prefab;
    private Queue<Enemy> queue;
    public Vector3 endPos { get; private set; }

    private void Awake()
    {
        endPos = new Vector3(0, 0, -gameObject.transform.position.z);
        queue = new Queue<Enemy>();
        Add(5);
    }

    public Enemy Get()
    {
        if (queue.Count == 0)
        {
            Add(1);
        }

        return queue.Dequeue();
    }

    private void Add(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Enemy e = Instantiate(prefab, transform);
            e.gameObject.SetActive(false);
            queue.Enqueue(e);
        }
    }

    public void ReturnToPool(Enemy e)
    {
        e.gameObject.SetActive(false);
        queue.Enqueue(e);
    }
}
