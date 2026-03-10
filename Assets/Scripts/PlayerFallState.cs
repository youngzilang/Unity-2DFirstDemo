using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVe(0, 0);

    }

    public override void Update()
    {
        base.Update();

        if (player.WallCheck())
        {
            stateMachine.ChangeState(player.slideState);
        }

        if (inputX != 0)
        {
            player.SetVe(inputX * player.moveSpeed * 0.8f, player.rb.velocity.y);
        }
        if (player.GroundCheck())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
