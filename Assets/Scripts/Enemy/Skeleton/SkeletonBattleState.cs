using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Enemy_Skeleton skeleton;
    private Transform player;
    private int dir;
    public SkeletonBattleState(Enemy enemy, EnemyStateMachine stateMachine, string aniName,Enemy_Skeleton skeleton) : base(enemy, stateMachine, aniName)
    {
        this.skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (skeleton.PlayerCheck())
        {
            stateTimer = skeleton.battleTime;
            if (skeleton.PlayerCheck().distance < skeleton.attackDistance&&CanAttack())
            {
                stateMachine.ChangeState(skeleton.attackState);
            }
        }
        else
        {
            if (stateTimer < 0|| Vector2.Distance(player.position, skeleton.transform.position) > 7) stateMachine.ChangeState(skeleton.idleState);
        }

        if(Vector2.Distance(player.transform.position, skeleton.rb.position)>1.5)
        {
            if (player.position.x > skeleton.rb.position.x) dir = 1;
            else dir = -1;
        }

        skeleton.SetVe(skeleton.moveSpeed * dir, skeleton.rb.velocity.y);
    }

    public bool CanAttack()
    {
        if (skeleton.lastAttackTime + skeleton.attackCd <= Time.time) return true;
        else return false;
    }
}
