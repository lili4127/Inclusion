using UnityEngine;

public class PlayerColor : Character
{
    [SerializeField] private ParticleSystem pSystem;
    [SerializeField] private AudioSource particleSound;

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
        rend.material.color = HelperClass.colors[i];
        activeMaterial = i;
    }

    public int GetActiveMaterial()
    {
        return activeMaterial;
    }
}
