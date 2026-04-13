using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BOSS : Enemy
{
    [Header("传送相关")]
    [SerializeField] private BoxCollider2D transformArea;
    [SerializeField] private Vector2 surroundingCheck;

    [Space]
    [Header("技能相关")]
    [SerializeField] private GameObject skillPrefab;
    public int skillCount;
    public float skillCd;
    [SerializeField] private float stateCd;
    public float lastSkillTime;

    public float transformChance;
    public float defaultTransformChance=25;

    public bool bossBegin;

    #region State

    public BOSSIdleState idleState { get; private set; }
    public BOSSSkillStartState skillStartState { get; private set; }
    public BOSSDeadState deadState { get; private set; }
    public BOSSAttackState attackState { get; private set; }
    public BOSSTransformState transformState { get; private set; }
    public BOSSBattleState battleState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        SetDefaultDir(-1);

        idleState = new BOSSIdleState(this, stateMachine, "idle", this);
        skillStartState = new BOSSSkillStartState(this, stateMachine, "skill", this);
        deadState = new BOSSDeadState(this, stateMachine, "idle", this);
        attackState = new BOSSAttackState(this, stateMachine, "attack", this);
        transformState = new BOSSTransformState(this, stateMachine, "transform", this);
        battleState = new BOSSBattleState(this, stateMachine, "move", this);
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

    public void CreateSkllPrefab()
    {
        float xOffset = 0;

        if(PlayerManager.instance.player.rb.velocity.x != 0)
        {
            xOffset += PlayerManager.instance.player.faceDir * 3;
        }

        Vector3 position= new Vector3(PlayerManager.instance.player.transform.position.x+ xOffset, PlayerManager.instance.player.transform.position.y+1.7f);

        GameObject newSkillPrefab= Instantiate(skillPrefab,position, Quaternion.identity);

        newSkillPrefab.GetComponent<BOSSSkillController>().SetUpStat(stats);
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

    private bool SomethingAround() => Physics2D.BoxCast(transform.position, surroundingCheck, 0, Vector2.zero, 0, playerMask);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheck);
    }

    public bool TransformCheck()
    {
        if(Random.Range(0, 100) <= transformChance)
        {
            transformChance = defaultTransformChance;
            return true;
        }
            
       return false;
    }

    public bool CanSkill()
    {
        if(lastSkillTime + stateCd <= Time.time)
        {
            return true;
        }
        else return false; 
    }
}
