using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    protected float moveSpeed;
    private float tiredness;
    private PlayerScriptable playerScriptable;
    private float turnSmoothVelocity;
    private Vector3 gravityVelocity;
    private PlayerView playerView;
    private PlayerStateMachine stateMachine;
    private State state;
    private int rockCount;
    public bool IsInteracted;
    private LTDescr tireNessleenTween;

    public PlayerController(PlayerView playerPrefab, PlayerScriptable playerScriptable)
    {
        this.playerView = Object.Instantiate(playerPrefab);
        playerView.SetController(this);
        this.playerScriptable = playerScriptable;
        playerView.controller = new CharacterController(); // Assuming you have a way to initialize this
        CreateStateMachine();
        stateMachine.ChangeState(PlayerStates.Idle);
        playerScriptable.tiredness = 0f;
    }

    public void Interact() => IsInteracted = Input.GetKeyDown(KeyCode.E) ? true : (Input.GetKeyUp(KeyCode.E) ? false : IsInteracted);

    private void CreateStateMachine() => stateMachine = new PlayerStateMachine(this);

    public void Update()
    {
        if (state == State.Activate)
        {
            GetInput();
            stateMachine.Update();
        }
    }

    public void GetInput()
    {
        float v = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.Space);
        moveSpeed = (v != 0) ? (!isRunning) ? playerScriptable.walkSpeed : playerScriptable.runSpeed : 0;
        PlayerMove(v);
    }

    public void SetTiredness(float val)
    {
        playerScriptable.tiredness += val * playerScriptable.tirednessIncRate;
        Mathf.Clamp(playerScriptable.tiredness, 0, playerScriptable.maxTiredness);
        UIManager.Instance.playerPanel.SetTiredness(playerScriptable.tiredness, playerScriptable.maxTiredness);
    }

    public void PlayerMove(float v)
    {
        // Get the camera's X-axis rotation value from the FreeLook component
        float cameraXAxisValue = playerView.cam.m_XAxis.Value;

        // Always rotate player to match camera's horizontal rotation
        float targetAngle = cameraXAxisValue;
        float angle = Mathf.SmoothDampAngle(playerView.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, playerScriptable.rotationSmoothTime);
        playerView.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        // Movement relative to camera direction
        if (new Vector3(0, 0, v).magnitude >= 0.1f)
        {
            Vector3 inputDir = new Vector3(0, 0f, v).normalized;

            // Calculate movement direction based on camera rotation
            float moveAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraXAxisValue;
            Vector3 moveDir = Quaternion.Euler(0f, moveAngle, 0f) * Vector3.forward;

            playerView.controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime * ((100 - playerScriptable.tiredness) / 100));

            SetTiredness(moveSpeed);
        }

        // Apply gravity
        if (playerView.controller.isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }

        gravityVelocity.y += playerScriptable.gravity * Time.deltaTime;
        playerView.controller.Move(gravityVelocity * Time.deltaTime);
    }

    public Animator GetPlayerAnimator()
    {
        return playerView.animator;
    }

    public void Activate()
    {
        state = State.Activate;
        playerView.gameObject.SetActive(true);
        playerView.cam.Priority = 1;

        if (tireNessleenTween != null)
        {
            LeanTween.cancel(tireNessleenTween.id);
            tireNessleenTween = null;
        }
    }

    public void Deactivate()
    {
        state = State.deactivate;
        playerView.gameObject.SetActive(false);
        playerView.cam.Priority = 0;
    }

    public void AddRock(RockType rockType)
    {
        if (GetTotalRock() < playerScriptable.bagCapacity)
        {
            RockData rockData = playerScriptable.rockDatas.Find(r => r.RockType == rockType);
            rockData.AddRock();
            UIManager.Instance.playerPanel.SetRockCount(rockType, rockData.rockCount);
        }
        else
        {
            UIManager.Instance.GetInfoHandler().ShowInstruction(InstructionType.BagFull);
        }
    }

    private int GetTotalRock()
    {
        int totalCount = 0;
        foreach (var rockData in playerScriptable.rockDatas)
        {
            totalCount += rockData.rockCount;
        }
        return totalCount;
    }

    public void CarryBagPack()
    {
        playerView.CarryBagPack();
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public PlayerScriptable GetPlayerScriptable()
    {
        return playerScriptable;
    }

    public void TakeRest()
    {
        float tirednessRecoverTime = playerScriptable.tirednessRecoverTime * (playerScriptable.maxTiredness / playerScriptable.maxTiredness);
        tireNessleenTween = LeanTween.value(playerScriptable.maxTiredness, 0, tirednessRecoverTime).setOnUpdate((float val) =>
        {
            playerScriptable.tiredness = val;
            UIManager.Instance.playerPanel.SetTiredness(playerScriptable.tiredness, playerScriptable.maxTiredness);
        }).setOnComplete(() => tireNessleenTween = null);
    }
}
