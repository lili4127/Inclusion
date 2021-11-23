using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    //[SerializeField] private GameObject particleEffect;
    [SerializeField] private Material[] materials;
    private Renderer rend;
    private Material yellow;
    private Material red;
    private Material green;
    private Material gray;
    private Material black;
    public int activeMaterial { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        yellow = materials[0];
        red = materials[1];
        green = materials[2];
        gray = materials[3];
        black = materials[4];
        activeMaterial = 0;
        rend.material = yellow;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            ChangeMaterial(0);
        }

        else if (Input.GetKeyUp(KeyCode.A))
        {
            ChangeMaterial(1);
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            ChangeMaterial(2);
        }

        else if (Input.GetKeyUp(KeyCode.D))
        {
            ChangeMaterial(3);
        }

        else if (Input.GetKeyUp(KeyCode.E))
        {
            ChangeMaterial(4);
        }

    }

    public void ChangeMaterial(int i)
    {
        if(i == activeMaterial)
        {
            return;
        }

        //Instantiate(particleEffect, transform.position, Quaternion.identity);
        Debug.Log("made it");

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
            case 3:
                rend.material = gray;
                activeMaterial = 3;
                break;
            case 4:
                rend.material = black;
                activeMaterial = 4;
                break;
        }
    }
}
