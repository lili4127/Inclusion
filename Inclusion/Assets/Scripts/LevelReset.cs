using UnityEngine;

public class LevelReset : MonoBehaviour
{
    public static event System.Action levelChange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement p))
        {
            levelChange?.Invoke();
        }
    }
}
