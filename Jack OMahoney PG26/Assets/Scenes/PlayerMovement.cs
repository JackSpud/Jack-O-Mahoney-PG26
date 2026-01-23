using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float slideSpeed = 12f;
    public float slideDuration = 0.4f;

    private bool isSliding = false;
    private float slideTimer;
    private Vector3 slideDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (!isSliding)
        {
            if (Input.GetKey(KeyCode.W)) movement += Vector3.forward;
            if (Input.GetKey(KeyCode.S)) movement += Vector3.back;
            if (Input.GetKey(KeyCode.A)) movement += Vector3.left;
            if (Input.GetKey(KeyCode.D)) movement += Vector3.right;

            // Start slide
            if (movement != Vector3.zero && Input.GetKeyDown(KeyCode.LeftControl))
            {
                isSliding = true;
                slideTimer = slideDuration;
                slideDirection = movement.normalized;
            }
        }
        // Jump (always allowed)
        if (Input.GetKey(KeyCode.Space))
            movement += Vector3.up;

        // Sliding logic
        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            transform.Translate(slideDirection * slideSpeed * Time.deltaTime);

            if (slideTimer <= 0f)
                isSliding = false;
        }
        else
        {
            transform.Translate(movement.normalized * speed * Time.deltaTime);
        }

    }
}
