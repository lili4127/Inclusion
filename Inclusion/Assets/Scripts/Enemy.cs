using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event System.Action<int> playerAdded;
    public static event System.Action<int> playerRemoved;
    private Renderer rend;
    private int activeMaterial;

    private EnemyPool enemyPool;
    private float difficulty;

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        enemyPool = GetComponentInParent<EnemyPool>();
        difficulty = PlayerPrefs.GetInt("difficulty", 1);
    }

    public void Move()
    {
        SetRandomMaterial();
        StartCoroutine(MoveCo(enemyPool.endPos, difficulty));
    }

    IEnumerator MoveCo(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        enemyPool.ReturnToPool(this);
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
            }

            else
            {
                playerAdded?.Invoke(this.activeMaterial);
            }
        }
    }
}
