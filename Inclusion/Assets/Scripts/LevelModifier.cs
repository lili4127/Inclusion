using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModifier : MonoBehaviour
{
    [SerializeField] private int level;
    private ObjectPool enemyPool;

    private void Start()
    {
        level = 5;
        enemyPool = FindObjectOfType<ObjectPool>();
        PlaceEnemies();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement p))
        {
            p.ChangeSpeed();
            p.transform.position = Vector3.zero;
            ChangeLevel();
        }
    }

    private void ChangeLevel()
    {
        level++;
        ClearLevel();
        PlaceEnemies();
    }

    private void PlaceEnemies()
    {
        for (int i = 0; i < level; i++)
        {
            Enemy e = enemyPool.Get();
            int r = Random.Range(25, 75);
            e.transform.position = new Vector3(0, 0, r);
            e.gameObject.SetActive(true);
        }
    }

    private void ClearLevel()
    {
        foreach (Transform t in enemyPool.transform)
        {
            if (t.gameObject.activeInHierarchy)
            {
                t.GetComponent<Enemy>().ResetEnemy();
            }
        }
    }
}
