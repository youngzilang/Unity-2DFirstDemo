using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSIdleState : EnemyState
{
    private Enemy_BOSS boss;
    public BOSSIdleState(Enemy enemy, EnemyStateMachine stateMachine, string aniName, Enemy_BOSS boss) : base(enemy, stateMachine, aniName)
    {
        this.boss = boss;   
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = boss.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.PlaySFX(1, enemy.transform);
    }

    public override void Update()
    {
        base.Update();
       

        if(Input.GetKeyDown(KeyCode.K))
        {
            stateMachine.ChangeState(boss.transformState);
        }
    }
}
