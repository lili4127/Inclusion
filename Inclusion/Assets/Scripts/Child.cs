using UnityEngine;

public class Child : MonoBehaviour
{
    [SerializeField] private ParticleSystem pSystem;
    private GameManager gameManager;
    private Rigidbody rb;
    private Vector3 playerPos;
    private float jumpForce;
    private float movementSpeed;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        playerPos = new Vector3(0, 0, 500f);
        jumpForce = 500f;
    }

    private void OnEnable()
    {
        PlayerMovement.playerJumped += SetJump;
        GameManager.gameBegin += StartGame;
        PlayerTrain.destroyChild += DestroyChild;
    }

    private void StartGame(int l)
    {
        movementSpeed = l;
    }

    private void Update()
    {
        if (transform.position.z >= playerPos.z)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            ResetChild();
        }

        if (gameManager.timerGoing)
        {
            transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
        }
    }

    private void SetJump(Vector3 pos, float force)
    {
        playerPos = pos;
        jumpForce = force;
    }

    private void DestroyChild(int playerMaterial)
    {
        var main = pSystem.main;
        main.startColor = HelperClass.colors[playerMaterial];
        pSystem.Play();
    }

    private void ResetChild()
    {
        playerPos = new Vector3(0, 0, 500f);
        jumpForce = 500f;
    }

    private void OnDisable()
    {
        PlayerMovement.playerJumped -= SetJump;
        GameManager.gameBegin -= StartGame;
        PlayerTrain.destroyChild -= DestroyChild;
    }
}
