using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BOSS : Enemy
{

    #region State

    public BOSSIdleState idleState { get; private set; }
   // public BOSSMoveState moveState { get; private set; }
    public BOSSSkillStartState skillStartState { get; private set; }
    public BOSSDeadState deadState { get; private set; }
    public BOSSAttackState attackState { get; private set; }
    public BOSSTransformState transformState { get; private set; }
    public BOSSBattleState battleState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new BOSSIdleState(this, stateMachine, "idle", this);
        //moveState = new BOSSMoveState(this, stateMachine, "move", this);
        skillStartState = new BOSSSkillStartState(this, stateMachine, "skill", this);
        deadState = new BOSSDeadState(this, stateMachine, "idle", this);
        attackState = new BOSSAttackState(this, stateMachine, "attack", this);
        transformState = new BOSSTransformState(this, stateMachine, "transform", this);
        battleState = new BOSSBattleState(this, stateMachine, "battle", this);
    }

    protected override void Start()
    {
            base.Start();
            stateMachine.Initialize(idleState);
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
