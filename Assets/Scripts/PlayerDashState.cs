using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        dashContinueTimer = player.dashContinue;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVe(0, 0);
    }

    public override void Update()
    {
        base.Update();

        player.SetVe(player.dashSpeed*player.faceDir, 0);

        if (dashContinueTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
