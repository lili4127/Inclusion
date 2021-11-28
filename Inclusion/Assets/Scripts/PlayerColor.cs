using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private ParticleSystem pSystem;
    [SerializeField] private AudioSource particleSound;
    private Renderer rend;
    public int activeMaterial { get; private set; }

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        SetRandomMaterial();
    }

    private void SetRandomMaterial()
    {
        int r = Random.Range(0, HelperClass.playerColors.Count);
        rend.material.color = HelperClass.playerColors[r];
        activeMaterial = r;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeMaterial(0);
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeMaterial(1);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeMaterial(2);
        }
    }

    private void ChangeMaterial(int i)
    {
        if(i == activeMaterial)
        {
            return;
        }

        pSystem.Play();
        particleSound.Play();
        rend.material.color = HelperClass.playerColors[i];
        activeMaterial = i;
    }
}
