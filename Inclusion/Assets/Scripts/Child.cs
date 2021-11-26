using UnityEngine;

public class Child : MonoBehaviour
{
    [SerializeField] private ParticleSystem pSystem;
    [SerializeField] private Vector3 startOffset;
    private GameManager gameManager;
    private Rigidbody rb;
    private Vector3 jumpPos;
    private float jumpForce;
    private float movementSpeed;

    private void Awake()
    {
        startOffset = Vector3.zero + transform.position;
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        jumpPos = new Vector3(0, 0, 500f);
        jumpForce = 0f;
    }

    private void OnEnable()
    {
        PlayerMovement.playerJumped += SetJump;
        GameManager.gameBegin += StartGame;
        PlayerTrain.destroyChild += DestroyChild;
        LevelModifier.speedUp += SpeedUp;
        LevelReset.levelChange += ChangeLevel;
    }

    private void StartGame(int l)
    {
        movementSpeed = l;
    }

    private void Update()
    {
        if (transform.position.z >= jumpPos.z)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            ResetJump();
        }

        if (gameManager.timerGoing)
        {
            transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
        }
    }

    private void SetJump(Vector3 pos, float force)
    {
        jumpPos = pos;
        jumpForce = force;
    }

    private void DestroyChild(int playerMaterial)
    {
        var main = pSystem.main;
        main.startColor = HelperClass.colors[playerMaterial];
        pSystem.Play();
    }

    private void SpeedUp()
    {
        if (movementSpeed < 10)
        {
            movementSpeed += 1;
        }
        return;
    }

    private void ChangeLevel()
    {
        transform.position = Vector3.zero + startOffset;
    }

    private void ResetJump()
    {
        jumpPos = new Vector3(0, 0, 500f);
    }

    private void OnDisable()
    {
        PlayerMovement.playerJumped -= SetJump;
        GameManager.gameBegin -= StartGame;
        PlayerTrain.destroyChild -= DestroyChild;
        LevelModifier.speedUp -= SpeedUp;
        LevelReset.levelChange -= ChangeLevel;
    }
}
