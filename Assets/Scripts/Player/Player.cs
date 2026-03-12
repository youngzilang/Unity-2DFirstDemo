using System.Collections;
using UnityEngine;


public class Player : Entity
{
    [Header("攻击时人物速度矢量")]
    public Vector2[] attackVe;


    [Header("移动数据")]
    public float moveSpeed;
    public float jumpForce;
    public float slideJumpSpeed;

    [Header("冲刺数据")]
    public float dashSpeed;
    public float dashContinue;
    public float dashCd;

    

    public bool isBusy { get; private set; } = false;

    public float dashCdTimer { get; private set; }


    #region States
    public PlayerNormalAttackState attackState { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "isIdle");
        moveState = new PlayerMoveState(this, stateMachine, "isMove");
        fallState = new PlayerFallState(this, stateMachine, "isJump");
        jumpState = new PlayerJumpState(this, stateMachine, "isJump");
        dashState = new PlayerDashState(this, stateMachine, "isDash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "isWallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "isWallJump");
        attackState = new PlayerNormalAttackState(this, stateMachine, "isAttack");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override  void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    
        DashNow();
    }

    public IEnumerator Busy(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds (_seconds);

        isBusy = false;

    }


    private void DashNow()
    {
        dashCdTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCdTimer < 0)
        {
            dashCdTimer = dashCd;
            stateMachine.ChangeState(dashState);
        }
    }

    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }

    
}
