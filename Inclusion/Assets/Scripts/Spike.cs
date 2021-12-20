using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerTrain>(out PlayerTrain p))
        {
            p.DestroyTrain();
        }
    }
}
