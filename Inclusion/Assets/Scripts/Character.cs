using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Renderer rend;
    protected int activeMaterial;

    private void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        SetRandomMaterial();
    }

    protected void SetRandomMaterial()
    {
        int r = Random.Range(0, HelperClass.colors.Count);
        rend.material.color = HelperClass.colors[r];
        activeMaterial = r;
    }
}
