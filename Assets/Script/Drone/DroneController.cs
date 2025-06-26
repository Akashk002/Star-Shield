using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
public class DroneController
{
    private float moveSpeed = 0f;
    private float yaw;
    private float pitch;
    private bool isRotating = false;
    private DroneView droneView;
    private DroneScriptable droneScriptable;
    private Transform initialPosition;
    private DroneState droneState;
    public bool IsInteracted;
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

        // Initialize rotation values from current orientation
        Vector3 euler = droneView.transform.eulerAngles;
        yaw = euler.y;
        pitch = euler.x;
        droneState = DroneState.Deactivate; // Start in deactivated state
    }

    public void Interact() => IsInteracted = Input.GetKeyDown(KeyCode.E) ? true : (Input.GetKeyUp(KeyCode.E) ? false : IsInteracted);

    public void Update()
    {
        if (droneState == DroneState.Activate)
        {
            Move();

        }
        if (GetDronetype() == DroneType.SecurityDrone)
        {
            if (droneState == DroneState.Surveillance)
            {
                Surveillancing();
            }
        }
        else
        {
            Interact();
        }
    }

    private void Move()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W) && moveSpeed < droneScriptable.maxSpeed)
        {
            moveSpeed += droneScriptable.AccelerationSpeed;
            moveDirection = droneView.transform.forward * moveSpeed;
            isRotating = true;
        }
        else
        if (Input.GetKey(KeyCode.S) && moveSpeed < droneScriptable.maxSpeed)
        {
            moveSpeed += droneScriptable.AccelerationSpeed;
            moveDirection = -droneView.transform.forward * moveSpeed;
            isRotating = true;
        }
        else
        {
            moveSpeed = 0f; // Stop moving if no key is pressed
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection = droneView.transform.up * droneScriptable.verticalSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveDirection = -droneView.transform.up * droneScriptable.verticalSpeed;
            }
        }

        droneView.rb.velocity = moveDirection;


        if (isRotating || Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * droneScriptable.rotationSpeed * droneScriptable.mouseSensitivity * Time.fixedDeltaTime;
            pitch -= mouseY * droneScriptable.rotationSpeed * droneScriptable.mouseSensitivity * Time.fixedDeltaTime;

            pitch = Mathf.Clamp(pitch, -droneScriptable.maxPitch, droneScriptable.maxPitch);

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

    private void Surveillancing()
    {
        // Lock X rotation by keeping rotation flat (optional if needed)
        Vector3 currentEuler = droneView.transform.eulerAngles;
        currentEuler.x = 0f; // Keeps drone upright (no pitch)
        droneView.transform.eulerAngles = currentEuler;

        // Apply Y-axis rotation (surveillance spin)
        float yRotation = droneScriptable.rotationSpeed / 3 * Time.deltaTime;
        droneView.transform.Rotate(Vector3.up * 30f * Time.deltaTime, Space.Self); // Rotate around local Y axis
    }



    public void Activate()
    {
        if (GetDronetype() == DroneType.SecurityDrone && droneState == DroneState.Surveillance) return;

        droneState = DroneState.Activate;
        droneView.cam.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        if (GetDronetype() == DroneType.SecurityDrone && droneState == DroneState.Surveillance) return;

        droneState = DroneState.Deactivate;
        droneView.cam.gameObject.SetActive(false);
        isRotating = false;

    }

    public void ToggleDroneSurveillanceState()
    {
        if (droneState == DroneState.Surveillance)
        {
            droneState = DroneState.Activate;
            Activate();
            return;
        }

        droneState = DroneState.Surveillance;
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

    public DroneScriptable GetDroneScriptable()
    {
        return droneScriptable;
    }

    public void AddRock(RockType rockType)
    {
        if (GetTotalRock() < droneScriptable.RockStorageCapacity)
        {
            RockData rockData = droneScriptable.rockDatas.Find(r => r.RockType == rockType);
            rockData.AddRock();
            UIManager.Instance.droneUIManager.SetRockCount(rockType, rockData.rockCount);
        }
        else
        {
            UIManager.Instance.GetInfoHandler().ShowInstruction(InstructionType.BagFull);
        }
    }

    private int GetTotalRock()
    {
        int totalCount = 0;
        foreach (var rockData in droneScriptable.rockDatas)
        {
            totalCount += rockData.rockCount;
        }
        return totalCount;
    }

    public DroneState GetDroneState()
    {
        return droneState;
    }
}

public enum DroneState
{
    Activate,
    Deactivate,
    Surveillance,
}


