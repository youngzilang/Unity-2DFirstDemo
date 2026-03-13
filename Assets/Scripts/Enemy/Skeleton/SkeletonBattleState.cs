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
        player = GameObject.Find("Player").transform;
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
            if (skeleton.PlayerCheck().distance < skeleton.attackDistance&&CanAttack())
            {
                stateMachine.ChangeState(skeleton.attackState);
            }
        }

        if (player.position.x > skeleton.rb.position.x) dir = 1;
        else dir = -1;

        skeleton.SetVe(skeleton.moveSpeed * dir, skeleton.rb.velocity.y);
    }

    public bool CanAttack()
    {
        if (skeleton.lastAttackTime + skeleton.attackCd <= Time.time) return true;
        else return false;
    }
}
