using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private ObjectPool enemyPool;
    public static event System.Action<int> playerAdded;
    public static event System.Action<int> playerRemoved;

    private void Awake()
    {
        enemyPool = GetComponentInParent<ObjectPool>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerColor>(out PlayerColor p))
        {
            if (this.activeMaterial == p.GetActiveMaterial())
            {
                playerRemoved?.Invoke(this.activeMaterial);
                ResetEnemy();
            }

            else
            {
                playerAdded?.Invoke(this.activeMaterial);
                ResetEnemy();
            }
        }
    }

    public void ResetEnemy()
    {
        this.gameObject.SetActive(false);
        enemyPool.ReturnToPool(this.gameObject);
    }
}
