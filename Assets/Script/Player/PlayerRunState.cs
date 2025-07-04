using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IStatePlayer
{
    public PlayerController Owner { get; set; }
    private IStateMachinePlayer stateMachine;
    private AudioSource audioSource;
    public PlayerRunState(IStateMachinePlayer stateMachine) => this.stateMachine = stateMachine;

    public void OnStateEnter()
    {
        Owner.GetPlayerAnimator().SetBool("isRunning", true);
    }

    public void Update()
    {
        if (Owner.GetMoveSpeed() > 0)
        {
            if (Owner.GetMoveSpeed() <= Owner.GetPlayerScriptable().walkSpeed)
            {
                stateMachine.ChangeState(PlayerStates.Walk);
            }
            //if (audioSource == null || !audioSource.isPlaying)
            //    audioSource = AudioManager.Instance.PlayLoopingAt(GameAudioType.PlayerRun, Owner.GetPos());
            //else
            //    audioSource.transform.position = Owner.GetPos();
        }
        else
        {
            stateMachine.ChangeState(PlayerStates.Idle);
        }
    }

    public void OnStateExit()
    {
        Owner.GetPlayerAnimator().SetBool("isRunning", false);
    }
}
