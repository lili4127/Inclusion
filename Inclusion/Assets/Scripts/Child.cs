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
    private Animator anim;
    private float jumpForce;
    private float startOffset;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        pSystem = bloodPrefab.GetComponent<ParticleSystem>();
        main1 = bloodPrefab.GetComponent<ParticleSystem>().main;
        main2 = bloodPrefab.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        main3 = bloodPrefab.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().main;
    }

    private void OnEnable()
    {
        GameManager.gameBegin += StartGame;
        PlayerMovement.playerJumped += Jump;
        PlayerMovement.playerSlide += Slide;
    }

    private void StartGame(int d)
    {
        jumpForce = 24f + d;
        Physics.gravity = new Vector3(0, -9.8f - d, 0);
        startOffset = Mathf.Abs(transform.position.z) / d;
    }

    private void Jump()
    {
        StartCoroutine(JumpCo());
    }

    private void Slide()
    {
        StartCoroutine(SlideCo());
    }

    IEnumerator JumpCo()
    {
        float time = 0f;

        while (time < startOffset)
        {
            time += Time.deltaTime;
            yield return null;
        }

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    IEnumerator SlideCo()
    {
        float time = 0f;

        while (time < startOffset)
        {
            time += Time.deltaTime;
            yield return null;
        }

        anim.SetBool("isSliding", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isSliding", false);
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
