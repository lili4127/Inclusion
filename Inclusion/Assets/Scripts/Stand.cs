using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    private ObjectPool standPool;

    private void Awake()
    {
        standPool = GetComponentInParent<ObjectPool>();
    }

    public void ResetStand()
    {
        this.gameObject.SetActive(false);
        standPool.ReturnToPool(this.gameObject);
    }
}
