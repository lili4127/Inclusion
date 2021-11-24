using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : Character
{
    [SerializeField] private List<GameObject> children;
    [SerializeField] private List<Renderer> childrenRenderers;
    private int activeChildren = 0;
    private List<int> activeChildColors;
    public static event System.Action gameLost;

    private void Awake()
    {
        activeChildColors = new List<int>();
        children = new List<GameObject>();

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            children.Add(transform.GetChild(i+1).gameObject);
            childrenRenderers.Add(children[i].GetComponentInChildren<Renderer>());
        }
    }

    private void OnEnable()
    {
        Enemy.playerAdded += AddToLine;
        Enemy.playerRemoved += RemoveFromLine;
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
        if (activeChildColors.Contains(playerMaterial))
        {
            return;
        }

        childrenRenderers[activeChildren].material = materials[playerMaterial];
        children[activeChildren].SetActive(true);
        activeChildren++;
        activeChildColors.Add(playerMaterial);
    }

    private void RemoveFromLine(int playerMaterial)
    {
        if (!activeChildColors.Contains(playerMaterial))
        {
            PlayEffect(playerMaterial, transform.position);
            this.gameObject.SetActive(false);
            ClearLine();
            gameLost?.Invoke();
            return;
        }

        int indexToRemove = activeChildColors.IndexOf(playerMaterial);
        PlayEffect(playerMaterial, children[indexToRemove].transform.position);
        activeChildren--;
        activeChildColors.RemoveAt(indexToRemove);
        TightenUpLine();
    }

    private void TightenUpLine()
    {
        for (int i = 0; i < children.Count; i++)
        {
            if (i < activeChildColors.Count)
            {
                childrenRenderers[i].material = materials[activeChildColors[i]];
                children[i].SetActive(true);
            }

            else
            {
                children[i].SetActive(false);
            }
        }
    }

    private void PlayEffect(int materialNumber, Vector3 pos)
    {
        GameObject spawnedVFX = Instantiate(particleEffects[materialNumber], pos + particleOffset, Quaternion.identity);
        Destroy(spawnedVFX, 1);
    }

    private void ClearLine()
    {
        if (activeChildren > 0)
        {
            for (int i = 0; i < activeChildren; i++)
            {
                PlayEffect(activeChildColors[i], children[i].transform.position);
                children[i].SetActive(false);
            }
        }

        activeChildColors.Clear();
        activeChildren = 0;
    }
}
