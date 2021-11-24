using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private ObjectPool enemyPool;
    public static event System.Action<int> playerAdded;
    public static event System.Action playerRemoved;

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
                PlayEffect(this.activeMaterial);
                playerRemoved?.Invoke();
                ResetEnemy();
            }

            else
            {
                playerAdded?.Invoke(this.activeMaterial);
                ResetEnemy();
            }
        }
    }

    private void PlayEffect(int materialNumber)
    {
        GameObject spawnedVFX = Instantiate(particleEffects[materialNumber], transform.position + particleOffset, Quaternion.identity);
        Destroy(spawnedVFX, 1);
    }

    public void ResetEnemy()
    {
        this.gameObject.SetActive(false);
        enemyPool.ReturnToPool(this.gameObject);
    }
}
