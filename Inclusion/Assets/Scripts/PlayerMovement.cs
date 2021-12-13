using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private Rigidbody rb;
    private float jumpForce;
    [SerializeField] private bool isGrounded;
    public static event System.Action playerJumped;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        GameManager.gameBegin += StartGame;
    }

    private void StartGame(int d)
    {
        jumpForce = 24f + d;
        Physics.gravity = new Vector3(0, -9.8f - d, 0);
    }

    private void Update()
    {
        if (gameManager.timerGoing && Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerJumped?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.contacts[0].normal == Vector3.up)
        {
            isGrounded = true;
        }
    }

    private void OnDisable()
    {
        GameManager.gameBegin -= StartGame;
    }
}
