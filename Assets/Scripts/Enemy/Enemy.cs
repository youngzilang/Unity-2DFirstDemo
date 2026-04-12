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
    private float originalSpeed;
    [SerializeField] protected LayerMask player;

    [Header("怪物眩晕")]
    public float stunMove;
    public float stunJump;
    public float stunTime;

    public float attackCd;
    public float minattackCd;
    public float maxattackCd;
    [HideInInspector]public float lastAttackTime;

    protected bool canBeStun;
    [SerializeField] protected GameObject stunSign;

    public FX fX { get; private set; }

    protected EnemyStateMachine stateMachine { get; private set; }

    public string lastAniBoolName;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        originalSpeed = moveSpeed;
    }

    protected override void Start()
    {
        base.Start();
        fX = GetComponent<FX>();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void SetAniBoolName(string _name)
    {
        lastAniBoolName = _name;
    }

    public override void SlowByIce(float _slowPercent, float _slowTime)
    {
        moveSpeed = moveSpeed * (1 - _slowPercent);
        animator.speed = 1 - _slowPercent;
        Invoke("SlowOver", _slowTime);
    }

    public override void SlowOver()
    {
        base.SlowOver();
        moveSpeed = originalSpeed;
    }

    public void FreezeEffect(float _continue) => StartCoroutine(FreezeTimeFor(_continue));
    public virtual void FreezeTime(bool isFreeze)
    {
        if (isFreeze)
        {
            moveSpeed = 0;
            animator.speed = 0;
        }
        else
        {
            moveSpeed = originalSpeed;
            animator.speed = 1;
        }
    }

    protected virtual IEnumerator FreezeTimeFor(float _seconds)
    {
            FreezeTime(true);
            yield return new WaitForSeconds(_seconds);
            FreezeTime(false);
    }

    #region Stun
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

    #endregion
    public RaycastHit2D PlayerCheck() => Physics2D.Raycast(transform.position, Vector2.right*faceDir,attackDistance,player);

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new(transform.position.x + attackDistance*faceDir, transform.position.y));
    }
}
