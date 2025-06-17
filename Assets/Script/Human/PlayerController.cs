using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private float moveSpeed = 0;
    private float walkSpeed = 6f;
    private float runSpeed = 12f;
    private float rotationSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector3 gravityVelocity;
    private float gravity = -9.81f;
    private PlayerView playerView;
    private PlayerStateMachine stateMachine;

    public PlayerController(PlayerView playerPrefab, float walkSpeed, float runSpeed)
    {
        this.playerView = Object.Instantiate(playerPrefab);
        playerView.SetController(this);
        this.walkSpeed = walkSpeed;
        this.runSpeed = runSpeed;
        playerView.controller = new CharacterController(); // Assuming you have a way to initialize this

        CreateStateMachine();
        stateMachine.ChangeState(PlayerStates.Idle);
    }

    private void CreateStateMachine() => stateMachine = new PlayerStateMachine(this);

    public void Update()
    {
        GetInput();
        stateMachine.Update();
    }

    public void GetInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.Space);

        if (h != 0 || v != 0)
        {
            if (!isRunning)
            {
                moveSpeed = walkSpeed;
                stateMachine.ChangeState(PlayerStates.Walk);
            }
            else
            {
                moveSpeed = runSpeed;
                stateMachine.ChangeState(PlayerStates.Run);
            }

        }
        else
        {
            moveSpeed = 0;
            stateMachine.ChangeState(PlayerStates.Idle);
        }

        PlayerMove(h, v);
    }
    public void PlayerMove(float h, float v)
    {
        // Get the camera's X-axis rotation value from the FreeLook component
        float cameraXAxisValue = playerView.freeLookCamera.m_XAxis.Value;

        // Always rotate player to match camera's horizontal rotation
        float targetAngle = cameraXAxisValue;
        float angle = Mathf.SmoothDampAngle(playerView.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
        playerView.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        // Movement relative to camera direction
        if (new Vector3(h, 0, v).magnitude >= 0.1f)
        {
            Vector3 inputDir = new Vector3(h, 0f, v).normalized;

            // Calculate movement direction based on camera rotation
            float moveAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraXAxisValue;
            Vector3 moveDir = Quaternion.Euler(0f, moveAngle, 0f) * Vector3.forward;
            playerView.controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        // Apply gravity
        if (playerView.controller.isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }

        gravityVelocity.y += gravity * Time.deltaTime;
        playerView.controller.Move(gravityVelocity * Time.deltaTime);
    }

    public Animator GetPlayerAnimator()
    {
        return playerView.animator;
    }
}
