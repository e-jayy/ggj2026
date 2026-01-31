using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump")]
    public float jumpForce = 7f;
    public float jumpBufferTime = 0.15f;

    [Header("Jump Feel")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Crouch Settings")]
    public Vector2 crouchOffset = new Vector2(0f, -0.25f);
    public Vector2 crouchSize = new Vector2(1f, 0.5f);
    public float maxCrouchTime = 2f;

    [Header("Animator")]
    public Animator animator; // Drag your player animator here

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private bool isGrounded = false;
    private float jumpBufferCounter;

    // Collider backup
    private Vector2 originalOffset;
    private Vector2 originalSize;

    // Crouch state
    private bool isCrouching = false;
    private float crouchTimer = 0f;

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

        // --- Handle Crouch ---
        HandleCrouch();

        // --- Update Animator ---
        UpdateAnimator();
    }

    private void HandleCrouch()
    {
        if (isGrounded)
        {
            // Press Shift → start crouch
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
                StartCrouch();
            }

            if (isCrouching)
            {
                crouchTimer += Time.deltaTime;

                // Release early → stand up
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    StopCrouch();
                }
                // Max crouch time reached → stand up
                else if (crouchTimer >= maxCrouchTime)
                {
                    StopCrouch();
                }
            }
        }
        else if (isCrouching)
        {
            // If in air, automatically stand
            StopCrouch();
        }
    }

    private void StartCrouch()
    {
        isCrouching = true;
        crouchTimer = 0f;

        boxCollider.offset = crouchOffset;
        boxCollider.size = crouchSize;
    }

    private void StopCrouch()
    {
        isCrouching = false;

        boxCollider.offset = originalOffset;
        boxCollider.size = originalSize;
    }

    private void UpdateAnimator()
    {
        if (animator == null) return;

        // Jumping animation: if player is moving upward
        animator.SetBool("isJumping", !isGrounded);

        // Crouching animation
        animator.SetBool("isCrouching", isCrouching);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Player has died");
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
