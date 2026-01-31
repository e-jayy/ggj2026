using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump")]
    public float jumpForce = 7f;
    public float jumpBufferTime = 0.15f;

    [Header("Jump Feel")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Collider Shift Settings")]
    public Vector2 crouchOffset = new Vector2(0f, -0.25f);
    public Vector2 crouchSize = new Vector2(1f, 0.5f);

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private bool isGrounded = false;
    private float jumpBufferCounter;

    // Store original collider values
    private Vector2 originalOffset;
    private Vector2 originalSize;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        originalOffset = boxCollider.offset;
        originalSize = boxCollider.size;
    }

    void Update()
    {
        // --- Jump Buffer Input ---
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // --- Perform Jump ---
        if (jumpBufferCounter > 0f && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            jumpBufferCounter = 0f;
        }

        // --- Better Jump Feel ---
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
        }

        // --- Shift Collider Change ---
        HandleColliderShift();
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Obstacle"))
    {
        Debug.Log("Player has died");
    }
}

    void HandleColliderShift()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            boxCollider.offset = crouchOffset;
            boxCollider.size = crouchSize;
        }
        else
        {
            boxCollider.offset = originalOffset;
            boxCollider.size = originalSize;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
