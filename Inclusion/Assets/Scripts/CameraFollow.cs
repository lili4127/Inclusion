using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(8, 1, target.position.z + 2f);
            //transform.LookAt(target);
        }
    }
}
