using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : Character
{
    [SerializeField] private Renderer[] childRenderers;
    [SerializeField] private List<int> childColors;

    private void Awake()
    {
        childColors = new List<int> { 9, 9, 9 };
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
        rend.material = materials[i];
        activeMaterial = i;
    }

    public int GetActiveMaterial()
    {
        return activeMaterial;
    }

    private void AddToLine(int playerMaterial)
    {
        Debug.Log(transform.childCount.ToString());
        if (childColors.Contains(playerMaterial))
        {
            return;
        }

        for (int i = 1; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeInHierarchy)
            {
                childRenderers[i-1].material = materials[playerMaterial];
                transform.GetChild(i).gameObject.SetActive(true);
                childColors[i-1] = playerMaterial;
                return;
            }
        }
    }

    //private void RemoveFromLine()
    //{
    //    for (int i = 1; i < 3; i++)
    //    {
    //        if (transform.GetChild(i).gameObject.activeInHierarchy)
    //        {
    //            for(int j = i; j < 3; j++)
    //            {
    //                switch (j)
    //                {
    //                    case 1:
    //                        child1Rend.material = materials[playerMaterial];
    //                        break;
    //                    case 2:
    //                        child2Rend.material = materials[playerMaterial];
    //                        break;
    //                    case 3:
    //                        child3Rend.material = materials[playerMaterial];
    //                        break;
    //                }
    //                transform.GetChild(j).gameObject.SetActive(false);
    //            }
    //            return;
    //        }
    //    }
    //}
}
