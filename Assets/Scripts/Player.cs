using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("ÒÆ¶¯Êý¾Ý")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Åö×²¼ì²â")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private float wallDistance;
    [SerializeField] private LayerMask layer;

    public int faceDir { get; private set; } = 1;
    public bool faceRight { get; private set; } = true;


    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "isIdle");
        moveState=new PlayerMoveState(this, stateMachine, "isMove");
        fallState = new PlayerFallState(this, stateMachine, "isJump");
        jumpState = new PlayerJumpState(this, stateMachine, "isJump");

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
    }
    

    public void SetVe(float _x,float _y)
    {
        rb.velocity = new Vector2(_x, _y);
        FlipController(_x);
    }
   
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

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+wallDistance*faceDir, wallCheck.position.y));
    }
}
