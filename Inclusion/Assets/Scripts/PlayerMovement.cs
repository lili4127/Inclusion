using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rb;
    private float movementSpeed = 400f;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
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
        if (movementSpeed < 900)
        {
            movementSpeed += 50;
        }

        return;
    }
}
