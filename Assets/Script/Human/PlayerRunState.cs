using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IStatePlayer
{
    public PlayerController Owner { get; set; }
    private IStateMachinePlayer stateMachine;

    public PlayerRunState(IStateMachinePlayer stateMachine) => this.stateMachine = stateMachine;

    public void OnStateEnter()
    {
        Owner.GetPlayerAnimator().SetBool("isRunning", true);
    }

    public void Update() { }

    public void OnStateExit()
    {
        Owner.GetPlayerAnimator().SetBool("isRunning", false);
    }
}
