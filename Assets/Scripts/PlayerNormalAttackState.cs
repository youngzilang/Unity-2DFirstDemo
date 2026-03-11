using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttackState : PlayerState
{
    private int attackCount ;
    private float mixAttackWindow=1.0f;
    private float attackTiming;

    public PlayerNormalAttackState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(attackCount>2||Time.time>= attackTiming + mixAttackWindow)
        {
            attackCount = 0;
        }
        player.animator.SetInteger( "attackCount",attackCount);

        float attackFace=player.faceDir;
        if (inputX != 0)
        {
            attackFace = inputX;
        }


        player.SetVe(player.attackVe[attackCount].x* attackFace, player.attackVe[attackCount].y);

        stateTimer = 0.08f;
    }

    public override void Exit()
    {
        base.Exit();
        attackCount++;
        attackTiming = Time.time;
        player.StartCoroutine("Busy", 0.15);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) player.SetVe(0, 0);

        if (trigger) stateMachine.ChangeState(player.idleState);
    }
}
