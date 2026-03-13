using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("¹ÖÎïÐÅÏ¢")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float idleTime;
    [SerializeField] public float attackDistance;
    [SerializeField] protected LayerMask player;

    public float attackCd;
    [HideInInspector]public float lastAttackTime;
    
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

    public RaycastHit2D PlayerCheck() => Physics2D.Raycast(transform.position, Vector2.right*faceDir,50.0f,player);

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new(transform.position.x + attackDistance*faceDir, transform.position.y));
    }
}
