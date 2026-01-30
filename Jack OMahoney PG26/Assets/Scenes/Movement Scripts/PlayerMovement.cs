using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Camera")]
    public Transform cameraTransform;

    [Header("Jump")]
    public float jumpForce = 5f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundMask;

    [Header("Slide")]
    public float slideSpeed = 12f;
    public float slideDuration = 0.4f;

    private Rigidbody rb;
    private bool isSliding;
    private float slideTimer;
    private Vector3 slideDirection;
    private Vector3 moveInput;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // keep upright
    }

    void Update()
    {
        // ----- Ground check -----
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 1.1f, groundMask);

        // ----- Read input (Update) -----
        Vector3 movement = Vector3.zero;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        if (!isSliding)
        {
            if (Input.GetKey(KeyCode.W)) movement += camForward;
            if (Input.GetKey(KeyCode.S)) movement -= camForward;
            if (Input.GetKey(KeyCode.A)) movement -= camRight;
            if (Input.GetKey(KeyCode.D)) movement += camRight;

            // Start slide
            if (movement != Vector3.zero && Input.GetKeyDown(KeyCode.LeftControl))
            {
                isSliding = true;
                slideTimer = slideDuration;
                slideDirection = movement.normalized;
            }
        }

        moveInput = movement.normalized;

        // ----- Jump -----
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // ----- Sliding -----
        if (isSliding)
        {
            slideTimer -= Time.fixedDeltaTime;
            rb.MovePosition(rb.position + slideDirection * slideSpeed * Time.fixedDeltaTime);

            if (slideTimer <= 0f)
                isSliding = false;
        }
        else
        {
            rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
        }

        // ----- Rotate player toward movement -----
        if (moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime));
        }
    }
}