using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
public class SpacecraftController
{
    private float moveSpeed = 0f;
    private bool startMoving;
    private float yaw;
    private float pitch;
    private bool isRotating = false;
    private SpacecraftView spacecraftView;
    private SpacecraftScriptable spacecraftSO;
    private Transform initialTransform;
    private State state;
    public SpacecraftController(SpacecraftScriptable spacecraftSO)
    {
        this.spacecraftView = Object.Instantiate(spacecraftSO.spacecraftView);
        this.spacecraftView.SetController(this);
        this.spacecraftSO = spacecraftSO;
    }

    public void Configure()
    {
        initialTransform = spacecraftView.transform;
        spacecraftView.rb.freezeRotation = true;
        // Lock cursor for mouse look
        Cursor.lockState = CursorLockMode.Locked;

        // Initialize rotation values from current orientation
        Vector3 euler = spacecraftView.transform.eulerAngles;
        yaw = euler.y;
        pitch = euler.x;

        Deactivate();
    }

    public void Update()
    {
        if (state == State.Activate)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 moveDirection = Vector3.zero;

        // --- Movement ---
        if (Input.GetKey(KeyCode.W) && moveSpeed < spacecraftSO.MaxSpeed)
        {
            moveSpeed += spacecraftSO.AccelerationSpeed;
            isRotating = true;
        }
        else
        if (Input.GetKey(KeyCode.S) && moveSpeed > 0)
        {
            moveSpeed -= spacecraftSO.BrakeSpeed;
        }

        moveDirection += spacecraftView.transform.forward * (int)moveSpeed;

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += spacecraftView.transform.up * spacecraftSO.VerticalSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection -= spacecraftView.transform.up * spacecraftSO.VerticalSpeed;
        }

        spacecraftView.rb.velocity = moveDirection;


        if (isRotating || Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * spacecraftSO.RotationSpeed * spacecraftSO.MouseSensitivity * Time.fixedDeltaTime;
            pitch -= mouseY * spacecraftSO.RotationSpeed * spacecraftSO.MouseSensitivity * Time.fixedDeltaTime;

            pitch = Mathf.Clamp(pitch, -spacecraftSO.MaxPitch, spacecraftSO.MaxPitch);

            Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
            spacecraftView.rb.MoveRotation(targetRotation);
        }
    }

    public void Activate()
    {
        state = State.Activate;
        spacecraftView.cam.Priority = 1;
    }

    public void Deactivate()
    {
        state = State.deactivate;
        spacecraftView.cam.Priority = 0;
        Reset();
    }

    private void Reset()
    {
        spacecraftView.transform.position = initialTransform.position;
        spacecraftView.transform.rotation = initialTransform.rotation;
        yaw = initialTransform.eulerAngles.y;
        pitch = initialTransform.eulerAngles.x;
        moveSpeed = 0f;
        isRotating = false;
    }
}
