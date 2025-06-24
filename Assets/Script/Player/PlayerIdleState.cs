using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IStatePlayer
{
    public PlayerController Owner { get; set; }
    private IStateMachinePlayer stateMachine;

    public PlayerIdleState(IStateMachinePlayer stateMachine) => this.stateMachine = stateMachine;

    public void OnStateEnter() { }

    public void Update()
    {
        if (Owner.GetMoveSpeed() > 0)
        {
            if (Owner.GetMoveSpeed() > Owner.GetPlayerScriptable().walkSpeed)
            {
                stateMachine.ChangeState(PlayerStates.Run);
            }
            else
            {
                stateMachine.ChangeState(PlayerStates.Walk);
            }
        }
    }

    public void OnStateExit() { }
}
