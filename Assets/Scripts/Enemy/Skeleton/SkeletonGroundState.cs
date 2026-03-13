using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{

    protected Enemy_Skeleton skeleton;

    protected Transform player;
    public SkeletonGroundState(Enemy enemy, EnemyStateMachine stateMachine, string aniName,Enemy_Skeleton skeleton) : base(enemy, stateMachine, aniName)
    {
        this.skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (skeleton.PlayerCheck()||Vector2.Distance(player.position,skeleton.transform.position)<2)
        {
            stateMachine.ChangeState(skeleton.battleState);
        }
    }
}
