using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
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

    [Header("碰撞检测")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private float wallDistance;
    [SerializeField] private LayerMask layer;


    public bool isBusy { get; private set; } = false;

    public float dashCdTimer { get; private set; }

    public int faceDir { get; private set; } = 1;
    public bool faceRight { get; private set; } = true;


    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

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

    private void Awake()
    {
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

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
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

    #region Ve
    public void SetVe(float _x, float _y)
    {
        rb.velocity = new Vector2(_x, _y); FlipController(_x);

    }
    #endregion

    #region Flip
    public void Flip()
    {
        faceDir = -faceDir;
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !faceRight) Flip();
        else if (_x < 0 && faceRight) Flip();
    }
    #endregion

    #region LayerCheck
    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallDistance * faceDir, wallCheck.position.y));
    }

    public bool GroundCheck() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, layer);

    public bool WallCheck() => Physics2D.Raycast(wallCheck.position, Vector2.right * faceDir, wallDistance, layer);

    #endregion 
}
