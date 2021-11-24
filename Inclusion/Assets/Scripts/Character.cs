using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected GameObject[] particleEffects;
    [SerializeField] protected Material[] materials;
    protected Vector3 particleOffset;
    protected Renderer rend;
    protected Material yellow;
    protected Material red;
    protected Material green;
    protected int activeMaterial;

    private void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        yellow = materials[0];
        red = materials[1];
        green = materials[2];
        particleOffset = new Vector3(0, 1, 0);
        SetRandomMaterial();
    }

    protected void SetRandomMaterial()
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
}
