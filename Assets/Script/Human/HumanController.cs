using UnityEngine;
using Cinemachine; // Add this namespace to access Cinemachine components

[RequireComponent(typeof(CharacterController))]
public class HumanController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float rotationSmoothTime = 0.1f;

    [Header("Cinemachine References")]
    public CinemachineFreeLook freeLookCamera; // Assign your FreeLook camera in inspector

    private CharacterController controller;
    private float turnSmoothVelocity;
    private Vector3 gravityVelocity;
    private float gravity = -9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // If you haven't assigned the camera in inspector, try to find it
        if (freeLookCamera == null)
        {
            freeLookCamera = FindObjectOfType<CinemachineFreeLook>();
            if (freeLookCamera == null)
            {
                Debug.LogError("No CinemachineFreeLook camera found in scene!");
            }
        }
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Get the camera's X-axis rotation value from the FreeLook component
        float cameraXAxisValue = freeLookCamera.m_XAxis.Value;

        // Always rotate player to match camera's horizontal rotation
        float targetAngle = cameraXAxisValue;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        // Movement relative to camera direction
        if (new Vector3(h, 0, v).magnitude >= 0.1f)
        {
            Vector3 inputDir = new Vector3(h, 0f, v).normalized;

            // Calculate movement direction based on camera rotation
            float moveAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraXAxisValue;
            Vector3 moveDir = Quaternion.Euler(0f, moveAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        // Apply gravity
        if (controller.isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }

        gravityVelocity.y += gravity * Time.deltaTime;
        controller.Move(gravityVelocity * Time.deltaTime);
    }
}