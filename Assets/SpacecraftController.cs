using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpacecraftController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 0f;
    public float maxSpeed = 50f;
    public float verticalSpeed = 5f;
    public float brakeSpeed = 5f;
    public float accelerationSpeed = 5f;

    [Header("Rotation")]
    public float rotationSpeed = 100f;
    public float mouseSensitivity = 1.5f;
    public float maxPitch = 80f;

    private Rigidbody rb;
    private bool startMoving;

    private float yaw;
    private float pitch;

    private bool isRotating = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Lock cursor for mouse look
        Cursor.lockState = CursorLockMode.Locked;

        // Initialize rotation values from current orientation
        Vector3 euler = transform.eulerAngles;
        yaw = euler.y;
        pitch = euler.x;
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.zero;

        // --- Movement ---
        if (Input.GetKey(KeyCode.W) && moveSpeed < maxSpeed)
        {
            moveSpeed += accelerationSpeed;
            isRotating = true;
        }
        else
        if (Input.GetKey(KeyCode.S) && moveSpeed > 0)
        {
            moveSpeed -= brakeSpeed;
        }

        moveDirection += transform.forward * moveSpeed;

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += transform.up * verticalSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection -= transform.up * verticalSpeed;
        }

        rb.velocity = moveDirection;


        if (isRotating || Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * rotationSpeed * mouseSensitivity * Time.fixedDeltaTime;
            pitch -= mouseY * rotationSpeed * mouseSensitivity * Time.fixedDeltaTime;

            pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);

            Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
            rb.MoveRotation(targetRotation);
        }
    }
}
