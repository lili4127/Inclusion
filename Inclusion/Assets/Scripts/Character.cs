using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected GameObject[] particleEffects;
    [SerializeField] protected Material[] materials;
    protected Vector3 particleOffset;
    protected Renderer rend;
    protected int activeMaterial;

    private void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        particleOffset = new Vector3(0, 1, 0);
        SetRandomMaterial();
    }

    protected void SetRandomMaterial()
    {
        int r = Random.Range(0, materials.Length);
        rend.material = materials[r];
        activeMaterial = r;
    }
}
