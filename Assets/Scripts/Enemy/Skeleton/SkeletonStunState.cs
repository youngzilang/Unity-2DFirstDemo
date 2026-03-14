using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunState : EnemyState
{
    private Enemy_Skeleton skeleton;

    public SkeletonStunState(Enemy enemy, EnemyStateMachine stateMachine, string aniName,Enemy_Skeleton skeleton) : base(enemy, stateMachine, aniName)
    {
        this.skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();

        skeleton.fX.InvokeRepeating("ColorFlash", 0, 0.1f);

        stateTimer = skeleton.stunTime;
        skeleton.rb.velocity = new Vector2(-skeleton.faceDir * skeleton.stunMove, skeleton.stunJump);
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.fX.Invoke("CancleFlash",0);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0) stateMachine.ChangeState(skeleton.idleState);
    }
}
