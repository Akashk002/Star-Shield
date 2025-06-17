using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IStatePlayer
{
    public PlayerController Owner { get; set; }
    private IStateMachinePlayer stateMachine;

    public PlayerIdleState(IStateMachinePlayer stateMachine) => this.stateMachine = stateMachine;

    public void OnStateEnter() { }

    public void Update() { }

    public void OnStateExit() { }
}
