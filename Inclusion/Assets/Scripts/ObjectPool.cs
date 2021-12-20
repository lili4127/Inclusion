using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private List<GameObject> list;
    public Vector3 endPos { get; private set; }

    private void Awake()
    {
        endPos = new Vector3(0, 0, -gameObject.transform.position.z);
        list = new List<GameObject>();
        Add(objectsToSpawn.Length);
    }

    public GameObject Get()
    {
        int r = Random.Range(0, list.Count);
        return list[r];
    }

    private void Add(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject g = Instantiate(objectsToSpawn[i], transform);
            g.gameObject.SetActive(false);
            list.Add(g);
        }
    }

    public void ReturnToPool(GameObject g)
    {
        g.transform.position = new Vector3(transform.position.x, g.transform.position.y, transform.position.z);
        g.gameObject.SetActive(false);
    }
}
