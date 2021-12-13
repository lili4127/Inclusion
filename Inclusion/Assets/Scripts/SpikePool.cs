using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePool : MonoBehaviour
{
    [SerializeField] private Spike prefab;
    private Queue<Spike> queue;
    public Vector3 endPos { get; private set; }

    private void Awake()
    {
        endPos = new Vector3(0, 0, -gameObject.transform.position.z);
        queue = new Queue<Spike>();
        Add(5);
    }

    public Spike Get()
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
            Spike s = Instantiate(prefab, transform);
            s.gameObject.SetActive(false);
            queue.Enqueue(s);
        }
    }

    public void ReturnToPool(Spike s)
    {
        s.transform.position = transform.position;
        s.gameObject.SetActive(false);
        queue.Enqueue(s);
    }
}
