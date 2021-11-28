using System.Collections.Generic;
using UnityEngine;

public class PlayerTrain : Character
{
    //VFX
    [SerializeField] private GameObject bloodPrefab;
    private ParticleSystem pSystem;
    private ParticleSystem.MainModule main1;
    private ParticleSystem.MainModule main2;
    private ParticleSystem.MainModule main3;

    //Children
    [SerializeField] private List<Child> children;
    [SerializeField] private List<Renderer> childrenRenderers;
    private int activeChildren = 0;
    private List<int> activeChildColors;
    public static event System.Action gameLost;

    private void Awake()
    {
        pSystem = bloodPrefab.GetComponent<ParticleSystem>();
        main1 = bloodPrefab.GetComponent<ParticleSystem>().main;
        main2 = bloodPrefab.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        main3 = bloodPrefab.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().main;

        activeChildColors = new List<int>();
        foreach (Renderer r in childrenRenderers)
        {
            r.enabled = false;
        }
    }

    private void OnEnable()
    {
        Enemy.playerAdded += AddToLine;
        Enemy.playerRemoved += RemoveFromLine;
    }

    private void AddToLine(int playerMaterial)
    {
        if (activeChildColors.Contains(playerMaterial))
        {
            return;
        }

        childrenRenderers[activeChildren].material.color = HelperClass.colors[playerMaterial];
        childrenRenderers[activeChildren].enabled = true;
        activeChildren++;
        activeChildColors.Add(playerMaterial);
    }

    private void RemoveFromLine(int playerMaterial)
    {
        if (!activeChildColors.Contains(playerMaterial))
        {
            DestroyTrain();
            return;
        }

        int indexToRemove = activeChildColors.IndexOf(playerMaterial);
        children[indexToRemove].PlayChildEffect(playerMaterial);
        activeChildColors.RemoveAt(indexToRemove);
        activeChildren--;
        TightenUpLine();
    }

    private void TightenUpLine()
    {
        for (int i = 0; i < childrenRenderers.Count; i++)
        {
            if (i < activeChildColors.Count)
            {
                childrenRenderers[i].material.color = HelperClass.colors[activeChildColors[i]];
                childrenRenderers[i].enabled = true;
            }

            else
            {
                childrenRenderers[i].enabled = false;
            }
        }
    }

    public void DestroyTrain()
    {
        //HelperClass.colors[playerMaterial]
        main1.startColor = rend.material.color;
        main2.startColor = rend.material.color;
        main3.startColor = rend.material.color;
        pSystem.Play();
        rend.enabled = false;
        ClearLine();
        gameLost?.Invoke();
    }

    private void ClearLine()
    {
        if (activeChildren > 0)
        {
            for (int i = 0; i < activeChildren; i++)
            {
                children[i].PlayChildEffect(activeChildColors[i]);
                childrenRenderers[i].enabled = false;
            }
        }

        activeChildColors.Clear();
        activeChildren = 0;
    }

    private void OnDisable()
    {
        Enemy.playerAdded -= AddToLine;
        Enemy.playerRemoved -= RemoveFromLine;
    }
}
