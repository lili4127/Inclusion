using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset = new Vector3(8, 1f, 0);

    private void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
