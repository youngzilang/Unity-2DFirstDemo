using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("怪物信息")]
     public float moveSpeed;
     public float idleTime;
     public float attackDistance;
    public float battleTime;
    [SerializeField] protected LayerMask player;

    [Header("怪物眩晕")]
    public float stunMove;
    public float stunJump;
    public float stunTime;

    public float attackCd;
    [HideInInspector]public float lastAttackTime;

    protected bool canBeStun;
    [SerializeField] protected GameObject stunSign;

    protected EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public void OpenStunWindow()
    {
        canBeStun = true;
        stunSign.SetActive(true);
    }

    public void CloseStunWindow()
    {
        canBeStun = false;
        stunSign.SetActive(false);
    }

    public virtual bool StunCheck()
    {
        if (canBeStun)
        {
            CloseStunWindow();
            return true;
        }
        return false;
    }

    public RaycastHit2D PlayerCheck() => Physics2D.Raycast(transform.position, Vector2.right*faceDir,attackDistance,player);

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new(transform.position.x + attackDistance*faceDir, transform.position.y));
    }
}
