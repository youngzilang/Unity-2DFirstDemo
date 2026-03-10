using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (inputX != 0 && inputX == -player.faceDir) stateMachine.ChangeState(player.idleState);


        if(inputY<0) player.SetVe(0, player.rb.velocity.y );
        else player.SetVe(0, player.rb.velocity.y * 0.8f);

        if (player.GroundCheck())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
