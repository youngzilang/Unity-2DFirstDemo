using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
   

    public SkeletonIdleState(Enemy enemy, EnemyStateMachine stateMachine, string aniName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, aniName, skeleton)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = skeleton.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(skeleton.moveState);
        }
    }
}
