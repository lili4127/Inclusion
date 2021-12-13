using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private SpikePool spikePool;
    private Vector3 slideSpike = new Vector3(0, 1, 0);
    private float difficulty;

    private void Awake()
    {
        spikePool = GetComponentInParent<SpikePool>();
        difficulty = PlayerPrefs.GetInt("difficulty", 1);
    }

    private void OnEnable()
    {
        if (Random.value > 0.5)
        {
            transform.rotation = Quaternion.identity;
            transform.position = transform.position + slideSpike;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerTrain>(out PlayerTrain p))
        {
            p.DestroyTrain();
        }
    }

    public void Move()
    {
        StartCoroutine(MoveCo(spikePool.endPos, difficulty));
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
        spikePool.ReturnToPool(this);
    }
}
