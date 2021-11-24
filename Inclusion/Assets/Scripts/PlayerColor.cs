using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : Character
{
    [SerializeField] private Renderer child1Rend;
    [SerializeField] private Renderer child2Rend;
    [SerializeField] private List<int> childColors;

    private void Awake()
    {
        childColors = new List<int> { 9, 9 };
    }

    private void OnEnable()
    {
        Enemy.playerAdded += AddToLine;
        //Enemy.playerRemoved += RemoveFromLine;
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

    private void ChangeMaterial(int i)
    {
        if(i == activeMaterial)
        {
            return;
        }

        GameObject spawnedVFX = Instantiate(particleEffects[3], transform.position + particleOffset, Quaternion.identity);
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

    public int GetActiveMaterial()
    {
        return activeMaterial;
    }

    private void AddToLine(int playerMaterial)
    {
        if (childColors.Contains(playerMaterial))
        {
            return;
        }

        for (int i = 1; i < 3; i++)
        {
            if (!transform.GetChild(i).gameObject.activeInHierarchy)
            {
                switch (i)
                {
                    case 1:
                        child1Rend.material = materials[playerMaterial];
                        break;
                    case 2:
                        child2Rend.material = materials[playerMaterial];
                        break;
                }
                transform.GetChild(i).gameObject.SetActive(true);
                childColors[i-1] = playerMaterial;
                return;
            }
        }
    }

    //private void RemoveFromLine()
    //{
    //    List<int> childColors = new List<int>();

    //    for (int i = 1; i < 3; i++)
    //    {
    //        if (transform.GetChild(i).gameObject.activeInHierarchy)
    //        {
    //            childColors.Add(transform.GetChild(0))
    //            transform.GetChild(i).gameObject.SetActive(false);
    //            return;
    //        }
    //    }
    //}
}
