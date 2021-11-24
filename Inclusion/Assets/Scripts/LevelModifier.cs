using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModifier : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private ObjectPool enemyPool;
    //[SerializeField] private ObjectPool standPool;

    private void Awake()
    {
        level = 10;
        enemyPool = enemyPool.GetComponent<ObjectPool>();
        //standPool = standPool.GetComponent<ObjectPool>();
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
            GameObject e = enemyPool.Get();
            e.transform.position = new Vector3(0, 0, 100/level * i);
            e.gameObject.SetActive(true);

            //int r = Random.Range(0, 11);
            //if (r <= 3)
            //{
            //    GameObject s = standPool.Get();
            //    s.transform.position = new Vector3(0, 1, 100 / level * i);
            //    e.transform.position += new Vector3(0, 1, 0);
            //    s.gameObject.SetActive(true);
            //}
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

        //foreach (Transform t in standPool.transform)
        //{
        //    if (t.gameObject.activeInHierarchy)
        //    {
        //        t.GetComponent<Stand>().ResetStand();
        //    }
        //}
    }
}
