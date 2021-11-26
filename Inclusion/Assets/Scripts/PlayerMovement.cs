using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3 startOffset;
    private GameManager gameManager;
    private Rigidbody rb;
    private float movementSpeed;
    private float jumpForce;
    private Vector3 grav;
    [SerializeField] private bool isGrounded;
    public static event System.Action<Vector3, float> playerJumped;

    private void Awake()
    {
        startOffset = Vector3.zero + transform.position;
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        GameManager.gameBegin += StartGame;
        LevelModifier.speedUp += SpeedUp;
        LevelReset.levelChange += ChangeLevel;
    }

    private void StartGame(int l)
    {
        movementSpeed = l;
        jumpForce = 20f + l;
        grav = new Vector3(0, -5f - l, 0);
        Physics.gravity = grav;
    }

    private void Update()
    {
        if (gameManager.timerGoing && Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerJumped?.Invoke(transform.position, jumpForce);
        }

        if (gameManager.timerGoing)
        {
           transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
        }
    }

    private void SpeedUp()
    {
        if (movementSpeed < 10)
        {
            movementSpeed += 1;
            jumpForce += 1;
            grav += Vector3.down;
            Physics.gravity = grav;
        }

        return;
    }

    private void ChangeLevel()
    {
        transform.position = Vector3.zero + startOffset;
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
        LevelModifier.speedUp -= SpeedUp;
        LevelReset.levelChange -= ChangeLevel;
    }
}
