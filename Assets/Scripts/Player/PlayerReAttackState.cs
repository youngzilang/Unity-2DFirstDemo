using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReAttackState : PlayerState
{
    public PlayerReAttackState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.reAttackTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, player.attackR);

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                if (collider.GetComponent<Enemy>().StunCheck())
                {
                    stateTimer = 10;
                    stateMachine.ChangeState(player.reAttackState);
                }
            }
        }

        if (stateTimer < 0 || trigger)
        {
            stateMachine.ChangeState(player.idleState);
        }

    }
}
