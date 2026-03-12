using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    Enemy_Skeleton skeleton;

    public SkeletonMoveState(Enemy enemy, EnemyStateMachine stateMachine, string aniName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, aniName)
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
    }

    public override void Update()
    {
        base.Update();
        skeleton.SetVe(skeleton.moveSpeed*skeleton.faceDir, skeleton.rb.velocity.y);

        if (!skeleton.GroundCheck() || skeleton.WallCheck())
        {
            skeleton.Flip();
            stateMachine.ChangeState(skeleton.idleState);
        }
    }
}
