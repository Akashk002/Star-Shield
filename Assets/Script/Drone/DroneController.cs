using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
public class DroneController
{
    private float yaw;
    private float pitch;
    private bool isRotating = false;
    private DroneView droneView;
    private DroneScriptable droneScriptable;
    private Transform initialPosition;
    private State state;
    public DroneController(DroneScriptable droneScriptable)
    {
        this.droneView = Object.Instantiate(droneScriptable.droneView);
        this.droneView.SetController(this);
        this.droneScriptable = droneScriptable;
    }

    public void Configure()
    {
        initialPosition = droneView.transform;
        droneView.rb.freezeRotation = true;
        // Lock cursor for mouse look
        Cursor.lockState = CursorLockMode.Locked;

        // Initialize rotation values from current orientation
        Vector3 euler = droneView.transform.eulerAngles;
        yaw = euler.y;
        pitch = euler.x;
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

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = droneView.transform.forward * droneScriptable.Speed;
            isRotating = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = droneView.transform.up * droneScriptable.VerticalSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = -droneView.transform.up * droneScriptable.VerticalSpeed;
        }

        droneView.rb.velocity = moveDirection;


        if (isRotating || Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * droneScriptable.RotationSpeed * droneScriptable.MouseSensitivity * Time.fixedDeltaTime;
            pitch -= mouseY * droneScriptable.RotationSpeed * droneScriptable.MouseSensitivity * Time.fixedDeltaTime;

            pitch = Mathf.Clamp(pitch, -droneScriptable.MaxPitch, droneScriptable.MaxPitch);

            Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
            droneView.rb.MoveRotation(targetRotation);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            Debug.Log("Mouse scrolled: " + scroll);

            // Example: Zoom a camera
            droneView.cam.fieldOfView -= scroll * 10f;
            droneView.cam.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 30f, 90f);
        }
    }
    public void Activate()
    {
        state = State.Activate;
        droneView.cam.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        state = State.deactivate;
        droneView.cam.gameObject.SetActive(false);
        isRotating = false;
        Reset();
    }

    private void Reset()
    {
        droneView.rb.velocity = Vector3.zero;
        droneView.transform.position = initialPosition.position;
        droneView.transform.rotation = initialPosition.rotation;
        Cursor.lockState = CursorLockMode.None; // Unlock cursor when deactivated
    }

    public DroneType GetDronetype()
    {
        return droneScriptable.droneType;
    }
}

