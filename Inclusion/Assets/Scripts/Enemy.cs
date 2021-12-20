using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event System.Action<int> playerAdded;
    public static event System.Action<int> playerRemoved;
    private Renderer rend;
    private int activeMaterial;

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        activeMaterial = HelperClass.playerColors.IndexOf(rend.material.color);
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

            this.gameObject.SetActive(false);
        }
    }
}
