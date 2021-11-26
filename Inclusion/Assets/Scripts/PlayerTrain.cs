using System.Collections.Generic;
using UnityEngine;

public class PlayerTrain : Character
{
    [SerializeField] private ParticleSystem bloodParticles;
    [SerializeField] private List<Renderer> childrenRenderers;
    private int activeChildren = 0;
    [SerializeField] private List<int> activeChildColors;
    public static event System.Action<int> destroyChild;
    public static event System.Action gameLost;

    private void Awake()
    {
        activeChildColors = new List<int>();

        //foreach(Renderer r in childrenRenderers)
        //{
        //    r.enabled = false;
        //}
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
            //Debug.Log(playerMaterial.ToString());
            //var main = bloodParticles.main;
            //main.startColor = HelperClass.colors[playerMaterial];
            //bloodParticles.Play();
            rend.enabled = false;
            ClearLine();
            gameLost?.Invoke();
            return;
        }
        destroyChild?.Invoke(playerMaterial);
        int indexToRemove = activeChildColors.IndexOf(playerMaterial);
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

    private void ClearLine()
    {
        if (activeChildren > 0)
        {
            for (int i = 0; i < activeChildren; i++)
            {
                destroyChild?.Invoke(activeChildColors[i]);
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
