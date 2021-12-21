using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private Animator anim;
    private Rigidbody rb;
    private CapsuleCollider col;
    private bool isGrounded;
    private bool isSliding = false;
    public static bool jumpPressed = false;
    private float jumpForce = 24f;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2.5f;
    public static event System.Action playerJumped;
    public static event System.Action playerSlide;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (gameManager.timerGoing && Input.GetKeyDown(KeyCode.W) && isGrounded && !isSliding)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerJumped?.Invoke();
        }

        if (gameManager.timerGoing && Input.GetKey(KeyCode.W))
        {
            jumpPressed = true;
        }

        else
        {
            jumpPressed = false;
        }

        if (gameManager.timerGoing && Input.GetKeyDown(KeyCode.E) && !isSliding)
        {
            playerSlide?.Invoke();
            StartCoroutine(SlideCo());
        }
    }

    private void FixedUpdate()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        else if (rb.velocity.y > 0 && !jumpPressed)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    IEnumerator SlideCo()
    {
        isSliding = true;
        anim.SetBool("isSliding", true);
        col.center = new Vector3(0, 0.5f, 0);
        col.height = 1f;
        yield return new WaitForSeconds(1f);
        col.center = new Vector3(0, 1f, 0);
        col.height = 2f;
        anim.SetBool("isSliding", false);
        isSliding = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.contacts[0].normal == Vector3.up)
        {
            isGrounded = true;
        }
    }
}
