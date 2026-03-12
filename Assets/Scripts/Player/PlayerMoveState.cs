using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetVe(inputX * player.moveSpeed, player.rb.velocity.y);
        player.FlipController(inputX);

        if (inputX == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
