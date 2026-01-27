using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;        // Player
    public float distance = 5f;
    public float mouseSensitivity = 3f;
    public float minY = -40f;
    public float maxY = 70f;

    private float currentX;
    private float currentY;

    void Start()
    {
        /// Lock cursor to center of screen preventing it from 
        /// leaving the game window and hide it. 

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        /// Update camera rotation based on mouse movement
        /// Mouse X ? rotate around the player
        /// Mouse Y ? tilt up/down
        /// Clamp ? prevents flipping upside-down
        currentX += Input.GetAxis("Mouse X") * mouseSensitivity;
        currentY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        currentY = Mathf.Clamp(currentY, minY, maxY);
    }

    void LateUpdate()
    {
        ///
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = target.position + rotation * direction;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
