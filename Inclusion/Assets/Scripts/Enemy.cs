using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject[] particleEffects;
    [SerializeField] private Material[] materials;
    private ObjectPool enemyPool;
    private Transform particlePos;
    private Renderer rend;
    private Material yellow;
    private Material red;
    private Material green;
    public int activeMaterial { get; private set; }
    public static event System.Action playerHit;

    // Start is called before the first frame update
    void Start()
    {
        enemyPool = GetComponentInParent<ObjectPool>();
        particlePos = transform.GetChild(1).transform;
        rend = GetComponentInChildren<Renderer>();
        yellow = materials[0];
        red = materials[1];
        green = materials[2];
        SetRandomMaterial();
    }

    private void SetRandomMaterial()
    {
        int r = Random.Range(0, materials.Length);
        rend.material = materials[r];

        switch (r)
        {
            case 0:
                activeMaterial = 0;
                break;
            case 1:
                activeMaterial = 1;
                break;
            case 2:
                activeMaterial = 2;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerColor>(out PlayerColor p))
        {
            if (p.activeMaterial == activeMaterial)
            {
                PlayEffect(p.activeMaterial);
                ResetEnemy();
            }

            else
            {
                playerHit?.Invoke();
                ResetEnemy();
            }
        }
    }

    private void PlayEffect(int materialNumber)
    {
        GameObject spawnedVFX = Instantiate(particleEffects[materialNumber], particlePos.position, Quaternion.identity);
        Destroy(spawnedVFX, 1);
    }

    public void ResetEnemy()
    {
        this.gameObject.SetActive(false);
        enemyPool.ReturnToPool(this);
    }
}
