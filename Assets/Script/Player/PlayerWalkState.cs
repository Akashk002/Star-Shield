using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IStatePlayer
{
    public PlayerController Owner { get; set; }
    private IStateMachinePlayer stateMachine;

    public PlayerWalkState(IStateMachinePlayer stateMachine) => this.stateMachine = stateMachine;

    public void OnStateEnter()
    {
        Owner.GetPlayerAnimator().SetBool("isWalking", true);
    }

    public void Update()
    {
        if (Owner.GetMoveSpeed() > 0)
        {
            if (Owner.GetMoveSpeed() > Owner.GetPlayerScriptable().walkSpeed)
            {
                stateMachine.ChangeState(PlayerStates.Run);
            }
        }
        else
        {
            stateMachine.ChangeState(PlayerStates.Idle);
        }
    }

    public void OnStateExit()
    {
        Owner.GetPlayerAnimator().SetBool("isWalking", false);
    }
}