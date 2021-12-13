using System.Collections;
using UnityEngine;

public class Child : MonoBehaviour
{
    [SerializeField] private GameObject bloodPrefab;
    private ParticleSystem pSystem;
    private ParticleSystem.MainModule main1;
    private ParticleSystem.MainModule main2;
    private ParticleSystem.MainModule main3;
    private Rigidbody rb;
    private float jumpForce;
    private float startOffset;
    private float difficulty;

    private void Awake()
    {
        startOffset = Mathf.Abs(transform.position.z) / 3;
        rb = GetComponent<Rigidbody>();
        pSystem = bloodPrefab.GetComponent<ParticleSystem>();
        main1 = bloodPrefab.GetComponent<ParticleSystem>().main;
        main2 = bloodPrefab.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        main3 = bloodPrefab.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().main;
    }

    private void OnEnable()
    {
        GameManager.gameBegin += StartGame;
        PlayerMovement.playerJumped += Jump;
    }

    private void StartGame(int d)
    {
        jumpForce = 24f + d;
        Physics.gravity = new Vector3(0, -9.8f - d, 0);
        difficulty = d;
    }

    private void Jump()
    {
        StartCoroutine(JumpCo());
    }

    IEnumerator JumpCo()
    {
        float time = 0f;

        while (time < startOffset/difficulty)
        {
            time += Time.deltaTime;
            yield return null;
        }

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void PlayChildEffect(int playerMaterial)
    {
        main1.startColor = HelperClass.particleColors[playerMaterial];
        main2.startColor = HelperClass.particleColors[playerMaterial];
        main3.startColor = HelperClass.particleColors[playerMaterial];
        pSystem.Play();
    }

    private void OnDisable()
    {
        GameManager.gameBegin -= StartGame;
    }
}
