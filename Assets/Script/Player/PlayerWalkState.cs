using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IStatePlayer
{
    public PlayerController Owner { get; set; }
    private IStateMachinePlayer stateMachine;
    private AudioSource walkAudioSource;
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

            if (walkAudioSource == null || !walkAudioSource.isPlaying)
                walkAudioSource = GameService.Instance.audioManager.PlayOneShotAt(GameAudioType.PlayerWalk, Owner.GetPos());
            else
                walkAudioSource.transform.position = Owner.GetPos();
        }
        else
        {
            stateMachine.ChangeState(PlayerStates.Idle);
        }
    }


    public void OnStateExit()
    {
        GameService.Instance.audioManager.StopSound(walkAudioSource);
        Owner.GetPlayerAnimator().SetBool("isWalking", false);
    }
}