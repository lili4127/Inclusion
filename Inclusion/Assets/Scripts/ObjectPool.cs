using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private Queue<GameObject> queue;

    private void Start()
    {
        queue = new Queue<GameObject>();
        Add(10);
    }

    public GameObject Get()
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
            GameObject g = Instantiate(prefab, transform);
            g.gameObject.SetActive(false);
            queue.Enqueue(g);
        }
    }

    public void ReturnToPool(GameObject g)
    {
        queue.Enqueue(g);
    }
}
