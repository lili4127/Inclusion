using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset = new Vector3(8f, 1f, 2f);

    private void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
