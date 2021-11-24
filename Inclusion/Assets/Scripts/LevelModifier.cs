using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModifier : MonoBehaviour
{
    [SerializeField] private int level;

    private void Awake()
    {
        level = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement p))
        {
            level++;
            p.ChangeSpeed();
        }
    }
}
