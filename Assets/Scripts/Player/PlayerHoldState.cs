using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldState : PlayerState
{
    private Transform returnSword;

    public PlayerHoldState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //调用接剑的dustFX效果
        player.fX.PlayDustFX();
        //调用接剑的screenShake效果
        player.fX.ScreenShake(player.fX.swordShake);

        returnSword = player.skillManager.swordSkill.swordOnly.transform;

        if (returnSword.position.x > player.transform.position.x && player.faceDir == -1) player.Flip();
        else if (returnSword.position.x < player.transform.position.x && player.faceDir == 1) player.Flip();

        player.rb.velocity=new Vector2(player.swordForce * -player.faceDir, player.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("Busy", .1f);
        player.SetVe(0, 0);
    }

    public override void Update()
    {
        base.Update();
        if (trigger)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
