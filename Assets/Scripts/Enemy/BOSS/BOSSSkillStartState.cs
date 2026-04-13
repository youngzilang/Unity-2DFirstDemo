using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSSkillStartState : EnemyState
{
    private Enemy_BOSS boss;

    private int skillCount;
    private float skillTimer;

    public BOSSSkillStartState(Enemy enemy, EnemyStateMachine stateMachine, string aniName, Enemy_BOSS boss) : base(enemy, stateMachine, aniName)
    {
        this.boss = boss;
    }

        public override void Enter()
        {
            base.Enter();

        skillCount = boss.skillCount;
        skillTimer = .5f;
    }

    public override void Update()
    {
        base.Update();

        skillTimer-= Time.deltaTime;

        if (CanUseSkill())
        {
            boss.CreateSkllPrefab();
        }
       
        if(skillCount<=0)stateMachine.ChangeState(boss.transformState);
    }

    public override void Exit()
    {
        base.Exit();

        boss.lastSkillTime = Time.time;
    }

    private bool CanUseSkill()
    {
        if (skillTimer < 0 && skillCount > 0)
        {
            skillTimer = boss.skillCd;
            skillCount--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
