using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundState
{
    

    public SkeletonMoveState(Enemy enemy, EnemyStateMachine stateMachine, string aniName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, aniName,skeleton)
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
        skeleton.SetVe(skeleton.moveSpeed*skeleton.faceDir, skeleton.rb.velocity.y);

        if (!skeleton.GroundCheck() || skeleton.WallCheck())
        {
            skeleton.Flip();
            stateMachine.ChangeState(skeleton.idleState);
        }
    }
}
