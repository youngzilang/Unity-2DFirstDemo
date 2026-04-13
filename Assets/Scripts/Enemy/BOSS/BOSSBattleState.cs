using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSBattleState : EnemyState
{
    private Transform player;
    private Enemy_BOSS boss;
    private int dir;

    public BOSSBattleState(Enemy enemy, EnemyStateMachine stateMachine, string aniName,Enemy_BOSS boss) : base(enemy, stateMachine, aniName)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;

        //  if (player.GetComponent<PlayerStat>().isDead) stateMachine.ChangeState(boss.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (boss.PlayerCheck())
        {
            stateTimer = boss.battleTime;
            if (boss.PlayerCheck().distance < boss.attackDistance )
            {
                if(CanAttack()) stateMachine.ChangeState(boss.attackState);
                else stateMachine.ChangeState(boss.idleState);
            } 
        }
       

        if (Vector2.Distance(player.transform.position, boss.rb.position) > 1.5)
        {
            if (player.position.x > boss.rb.position.x) dir = 1;
            else dir = -1;
        }

        if (boss.PlayerCheck() && boss.PlayerCheck().distance < boss.attackDistance-.1f) return;
        

        boss.SetVe(boss.moveSpeed * dir, boss.rb.velocity.y);
    }

    public bool CanAttack()
    {
        if (boss.lastAttackTime + boss.attackCd <= Time.time)
        {
            boss.attackCd = Random.Range(boss.minattackCd, boss.maxattackCd);
            boss.lastAttackTime = Time.time;
            return true;
        }
        else return false;
    }
}
