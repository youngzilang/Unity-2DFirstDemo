using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSDeadState : EnemyState
{
    private Enemy_BOSS boss;
    public BOSSDeadState(Enemy enemy, EnemyStateMachine stateMachine, string aniName, Enemy_BOSS boss) : base(enemy, stateMachine, aniName)
    {
        this.boss = boss;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        boss.animator.SetBool(boss.lastAniBoolName, true);
        boss.animator.speed = 0;
        boss.capsule.enabled = false;

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
            boss.rb.velocity = new Vector2(0, 5);
        }
    }
}