using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rb;
    [SerializeField] private float movementSpeed = 100f;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.z > 150f)
        {
            transform.position = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.timerGoing)
        {
            rb.velocity = Vector3.forward * movementSpeed * Time.fixedDeltaTime;
        }
    }

    public void ChangeSpeed()
    {
        movementSpeed += 50;
    }
}
