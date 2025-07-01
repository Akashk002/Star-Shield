using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
public class SpacecraftController
{
    private float moveSpeed = 0f;
    private float range = 0f;
    private float missileCount = 0f;
    private float yaw;
    private float pitch;
    private bool isRotating = false;
    private SpacecraftView spacecraftView;
    private SpacecraftScriptable spacecraftSO;
    private Transform initialTransform;
    private State state;
    private Vector3 currentTargetPosition;

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
        missileCount = spacecraftSO.missileCapacity;
        range = spacecraftSO.maxRange;
        // Lock cursor for mouse look
        //Cursor.lockState = CursorLockMode.Locked;

        // Initialize rotation values from current orientation
        Vector3 euler = spacecraftView.transform.eulerAngles;
        yaw = euler.y;
        pitch = euler.x;

        Activate();
    }

    public void Update()
    {
        if (state != State.Activate) return;

        if (Input.GetMouseButtonDown(0))
        {
            AimAtTarget();
            FireMissileAtTarget();
        }

        Move();
    }

    public void SetRange(float value)
    {
        range -= Time.deltaTime * value * 0.0001f;
        Mathf.Clamp(range, 0f, spacecraftSO.maxRange);
    }

    private void Move()
    {
        Vector3 moveDirection = Vector3.zero;

        // --- Throttle Forward/Backward ---
        bool isAccelerating = Input.GetKey(KeyCode.W) && moveSpeed < spacecraftSO.maxSpeed;
        bool isBraking = Input.GetKey(KeyCode.S) && moveSpeed > 0;

        if (isAccelerating)
        {
            moveSpeed += spacecraftSO.accelerationSpeed;
            isRotating = true;
        }
        else if (isBraking)
        {
            moveSpeed -= spacecraftSO.brakeSpeed;
        }

        // --- Forward Movement ---
        moveDirection += spacecraftView.transform.forward * moveSpeed;

        SetRange(moveSpeed);

        // --- Vertical Movement ---
        if (Input.GetKey(KeyCode.A))
            moveDirection += spacecraftView.transform.up * spacecraftSO.verticalSpeed;

        if (Input.GetKey(KeyCode.D))
            moveDirection -= spacecraftView.transform.up * spacecraftSO.verticalSpeed;

        // --- Apply Velocity ---
        spacecraftView.rb.velocity = moveDirection;

        // --- Rotation by Mouse ---
        if (isRotating || Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * spacecraftSO.rotationSpeed * spacecraftSO.mouseSensitivity * Time.fixedDeltaTime;
            pitch -= mouseY * spacecraftSO.rotationSpeed * spacecraftSO.mouseSensitivity * Time.fixedDeltaTime;
            pitch = Mathf.Clamp(pitch, -spacecraftSO.maxPitch, spacecraftSO.maxPitch);

            Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
            spacecraftView.rb.MoveRotation(targetRotation);
        }
    }


    public void Activate()
    {
        state = State.Activate;
        spacecraftView.cam.Priority = 1;
        UIManager.Instance.ShowPanel(PanelType.Spacecraft);
        UIManager.Instance.minimapIconPanel.SetTarget(spacecraftView.transform);
    }

    public void Deactivate()
    {
        state = State.deactivate;
        spacecraftView.cam.Priority = 0;
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

    internal void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    private void AimAtTarget()
    {
        // Get layer mask that excludes "Player"
        int playerLayer = LayerMask.NameToLayer("Player");
        int ignorePlayerMask = ~(1 << playerLayer); // Invert to ignore Player

        // Get ray from camera center
        Ray ray = spacecraftView.Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5000f, ignorePlayerMask))
        {
            currentTargetPosition = hit.point;
        }
    }

    private void FireMissileAtTarget()
    {
        if (missileCount > 0)
        {
            Transform shootPoint = spacecraftView.GetShootTransform();
            GameService.Instance.missileService.CreateMissile(spacecraftSO.missileType, shootPoint, currentTargetPosition, false);
        }
    }


    public float GetCurrentSpeed()
    {
        return moveSpeed;
    }

    public float GetMissileCount()
    {
        return missileCount;
    }

    internal SpacecraftScriptable GetSpacecraftScriptable()
    {
        return spacecraftSO;
    }

    public int GetCurrentRange()
    {
        return (int)range;
    }

    public int GetCurrentAltitude()
    {
        if (spacecraftView)
        {
            return (int)spacecraftView.transform.position.y;
        }
        return 0;
    }

    internal void Destroy()
    {
        SpacecraftView.Destroy(spacecraftView.gameObject);
    }
}
