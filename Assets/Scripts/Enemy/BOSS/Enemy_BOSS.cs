using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BOSS : Enemy
{
    [Header("´«ËÍÏà¹Ø")]
    [SerializeField] private BoxCollider2D transformArea;
    [SerializeField] private Vector2 surroundingCheck;

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

    public void FindPosition()
    {
        Vector2 randomPos = new Vector2(Random.Range(transformArea.bounds.min.x+3, transformArea.bounds.max.x-3), Random.Range(transformArea.bounds.min.y, transformArea.bounds.max.y-3));
        transform.position = randomPos;

        transform.position = new Vector2(transform.position.x, transform.position.y - GroundBelow().distance);

        if(SomethingAround()|| !GroundBelow() )
        {
            FindPosition();
        }
    }

    private RaycastHit2D GroundBelow()=> Physics2D.Raycast(transform.position,  Vector2.down, 100,layer);

    private bool SomethingAround() => Physics2D.BoxCast(transform.position, surroundingCheck, 0, Vector2.zero, 0, player);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheck);
    }
}
