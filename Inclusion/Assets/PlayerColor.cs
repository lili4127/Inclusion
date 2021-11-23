using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private Material[] materials;
    private Transform particlePos;
    private Renderer rend;
    private Material yellow;
    private Material red;
    private Material green;
    public int activeMaterial { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        particlePos = transform.GetChild(1).transform;
        yellow = materials[0];
        red = materials[1];
        green = materials[2];
        activeMaterial = 0;
        rend.material = yellow;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            ChangeMaterial(0);
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            ChangeMaterial(1);
        }

        else if (Input.GetKeyUp(KeyCode.D))
        {
            ChangeMaterial(2);
        }

    }

    public void ChangeMaterial(int i)
    {
        if(i == activeMaterial)
        {
            return;
        }

        GameObject spawnedVFX = Instantiate(particleEffect, particlePos.position, Quaternion.identity);
        Destroy(spawnedVFX, 1);

        switch (i)
        {
            case 0:
                rend.material = yellow;
                activeMaterial = 0;
                break;
            case 1:
                rend.material = red;
                activeMaterial = 1;
                break;
            case 2:
                rend.material = green;
                activeMaterial = 2;
                break;
        }
    }
}
