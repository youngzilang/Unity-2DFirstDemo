using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    

    #region State
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonIdleState idleState { get; private set; } 

    public SkeletonAttackState attackState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "isIdle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "isMove", this);
        battleState = new SkeletonBattleState(this, stateMachine, "isMove", this);
        attackState = new SkeletonAttackState(this, stateMachine, "isAttack", this);
       
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
