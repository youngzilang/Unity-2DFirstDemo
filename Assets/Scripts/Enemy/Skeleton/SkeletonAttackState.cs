using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private Enemy_Skeleton skeleton;
    public SkeletonAttackState(Enemy enemy, EnemyStateMachine stateMachine, string aniName,Enemy_Skeleton skeleton) : base(enemy, stateMachine, aniName)
    {
        this.skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.lastAttackTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        skeleton.SetVe(0, 0);

        if (trigger) stateMachine.ChangeState(skeleton.battleState);
    }
}
