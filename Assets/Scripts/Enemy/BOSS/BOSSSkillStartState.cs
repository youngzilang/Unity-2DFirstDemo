using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSSkillStartState : EnemyState
{
    private Enemy_BOSS boss;
    public BOSSSkillStartState(Enemy enemy, EnemyStateMachine stateMachine, string aniName, Enemy_BOSS boss) : base(enemy, stateMachine, aniName)
    {
        this.boss = boss;
    }
}
