using System.Collections;
using UnityEngine;

public class BOSSAttackState : EnemyState
{
    private Enemy_BOSS boss;
    public BOSSAttackState(Enemy enemy, EnemyStateMachine stateMachine, string aniName, Enemy_BOSS boss) : base(enemy, stateMachine, aniName)
    {
        this.boss =boss;
    }

    public override void Enter()
    {
        base.Enter();
        boss.transformChance += 5;
    }

    public override void Exit()
    {
        base.Exit();
        boss.lastAttackTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (!boss.isHit)
            boss.SetVe(0, 0);

        if (trigger)
        {
            if(boss.TransformCheck()) stateMachine.ChangeState(boss.transformState);
            else stateMachine.ChangeState(boss.battleState);
        }
    }
}