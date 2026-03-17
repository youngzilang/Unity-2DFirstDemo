using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.UpdateFaceDirection(inputX);
    }

    public override void Update()
    {
        base.Update();
        player.UpdateFaceDirection(inputX);

        if (Input.GetKeyDown(KeyCode.Mouse1)) stateMachine.ChangeState(player.aimState);

        if (Input.GetKeyDown(KeyCode.R)) stateMachine.ChangeState(player.reAttackState);

        if (Input.GetKeyDown(KeyCode.Mouse0)) stateMachine.ChangeState(player.attackState);


        if (player.rb.velocity.y < -0.01)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (Input.GetKeyDown(KeyCode.Space)&&player.GroundCheck())
        {
            stateMachine.ChangeState(player.jumpState);
        }

        
    }
}
