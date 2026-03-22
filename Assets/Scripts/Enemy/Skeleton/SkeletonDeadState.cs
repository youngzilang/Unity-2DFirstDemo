using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    private Enemy_Skeleton skeleton;
    public SkeletonDeadState(Enemy enemy, EnemyStateMachine stateMachine, string aniName, Enemy_Skeleton enemy_) : base(enemy, stateMachine, aniName)
    {
        skeleton = enemy_;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        skeleton.animator.SetBool(skeleton.lastAniBoolName, true);
        skeleton.animator.speed = 0;
        skeleton.capsule.enabled = false;

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer > 0)
        {
            skeleton.rb.velocity = new Vector2(0, 10);
        }
    }
}
