using UnityEngine;

public class Child : MonoBehaviour
{
    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private Vector3 startOffset;
    private ParticleSystem pSystem;
    private ParticleSystem.MainModule main1;
    private ParticleSystem.MainModule main2;
    private ParticleSystem.MainModule main3;
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

        pSystem = bloodPrefab.GetComponent<ParticleSystem>();
        main1 = bloodPrefab.GetComponent<ParticleSystem>().main;
        main2 = bloodPrefab.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        main3 = bloodPrefab.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().main;
    }

    private void OnEnable()
    {
        PlayerMovement.playerJumped += SetJump;
        GameManager.gameBegin += StartGame;
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

    public void PlayChildEffect(int playerMaterial)
    {
        main1.startColor = HelperClass.particleColors[playerMaterial];
        main2.startColor = HelperClass.particleColors[playerMaterial];
        main3.startColor = HelperClass.particleColors[playerMaterial];
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
        LevelModifier.speedUp -= SpeedUp;
        LevelReset.levelChange -= ChangeLevel;
    }
}
