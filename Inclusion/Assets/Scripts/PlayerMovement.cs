using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isSliding = false;
    private Animator anim;
    private Rigidbody rb;
    private CapsuleCollider col;
    private float jumpForce;
    public static event System.Action playerJumped;
    public static event System.Action playerSlide;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
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
        if (gameManager.timerGoing && Input.GetKeyDown(KeyCode.W) && isGrounded && !isSliding)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerJumped?.Invoke();
        }

        else if (gameManager.timerGoing && Input.GetKeyDown(KeyCode.E) && !isSliding)
        {
            playerSlide?.Invoke();
            StartCoroutine(SlideCo());
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

    private void OnDisable()
    {
        GameManager.gameBegin -= StartGame;
    }
}
