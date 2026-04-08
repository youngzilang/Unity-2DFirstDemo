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
        AudioManager.instance.PlaySFX(11, null);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(11);
    }

    public override void Update()
    {
        base.Update();

        player.SetVe(inputX * player.moveSpeed, player.rb.velocity.y);


        if (inputX == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
