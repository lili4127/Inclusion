using UnityEngine;

public class Enemy : MonoBehaviour
{
    private ObjectPool enemyPool;
    public static event System.Action<int> playerAdded;
    public static event System.Action<int> playerRemoved;
    private Renderer rend;
    private int activeMaterial;

    private void Awake()
    {
        enemyPool = GetComponentInParent<ObjectPool>();
        rend = GetComponentInChildren<Renderer>();
        SetRandomMaterial();
    }

    private void SetRandomMaterial()
    {
        int r = Random.Range(0, HelperClass.playerColors.Count);
        rend.material.color = HelperClass.playerColors[r];
        activeMaterial = r;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerColor>(out PlayerColor p))
        {
            if (this.activeMaterial == p.activeMaterial)
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
