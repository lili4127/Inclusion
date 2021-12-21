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
    private float jumpForce = 24f;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2.5f;
    private float startOffset;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        pSystem = bloodPrefab.GetComponent<ParticleSystem>();
        main1 = bloodPrefab.GetComponent<ParticleSystem>().main;
        main2 = bloodPrefab.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        main3 = bloodPrefab.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().main;
        startOffset = Mathf.Abs(transform.position.z)/3;
    }

    private void OnEnable()
    {
        PlayerMovement.playerJumped += Jump;
        PlayerMovement.playerSlide += Slide;
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

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        else if (rb.velocity.y > 0 && !PlayerMovement.jumpPressed)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
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
        PlayerMovement.playerJumped -= Jump;
        PlayerMovement.playerSlide -= Slide;
    }
}
