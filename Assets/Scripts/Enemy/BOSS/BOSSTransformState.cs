using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSTransformState : EnemyState
{
    private Enemy_BOSS boss;
    public BOSSTransformState(Enemy enemy, EnemyStateMachine stateMachine, string aniName, Enemy_BOSS boss) : base(enemy, stateMachine, aniName)
    {
        this.boss = boss;
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
    }
}