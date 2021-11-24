using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModifier : MonoBehaviour
{
    [SerializeField] private int level;
    private ObjectPool enemyPool;

    private void Awake()
    {
        level = 10;
        enemyPool = FindObjectOfType<ObjectPool>();
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
        if (level < 11)
        {
            level++;
        }

        ClearLevel();
        PlaceEnemies();
    }

    public void PlaceEnemies()
    {
        for (int i = 1; i < level; i++)
        {
            Enemy e = enemyPool.Get();
            e.transform.position = new Vector3(0, 0, 100/level * i);
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
