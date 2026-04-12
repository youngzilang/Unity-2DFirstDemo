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

        if (trigger) stateMachine.ChangeState(boss.battleState);
    }
}