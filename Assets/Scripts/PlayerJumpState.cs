using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerGroundState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.rb.velocity = new Vector2(0 , player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
       
    }

    public override void Update()
    {
        base.Update();
        if (inputX != 0)
        {
            player.SetVe(inputX * player.moveSpeed * 0.8f, player.rb.velocity.y);
        }

        if (player.WallCheck())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }

        if (player.rb.velocity.y<0)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
