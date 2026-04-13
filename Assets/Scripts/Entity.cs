using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("碰撞检测")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float groundDistance;
    [SerializeField] protected float wallDistance;
    [SerializeField] protected LayerMask layer;
    public Transform attackCheck;
    public float attackR;

    [Header("击退效果")]
    [SerializeField] protected float hitMove;
    [SerializeField] protected float hitJump;
    [SerializeField] protected float hitTime;
    [HideInInspector]public bool isHit;


    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public CapsuleCollider2D capsule { get; private set; }
    public CharaterStats stats { get; private set; }
    #endregion


    public int faceDir { get; private set; } = 1;
    protected bool faceRight { get; private set; } = true;

    public int knocBackDir { get; private set; }

    public Action onFlip;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharaterStats>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    protected virtual void Update()
    {

    }

    public virtual void SlowByIce(float _slowPercent,float _slowTime)
    {
        
    }

    public virtual void SlowOver()
    {
        animator.speed = 1;
    }

    public virtual void Die()
    {

    }

    public void Damage() => StartCoroutine("Hitted");

    public void SetDefaultDir(int _dir)
    {
        faceDir= _dir;
        if(_dir==1) faceRight = true;
        else faceRight = false;
    }


    public IEnumerator Hitted()
    {
        isHit = true;

        rb.velocity = new Vector2(hitMove * knocBackDir, hitJump);

        yield return new WaitForSeconds(hitTime);

        isHit = false;

        SetUpZeroKnockBack();
    }

    public void SetUpKnokBackPower(int _hitMove,int _hitJump)
    {
        hitMove = _hitMove;
        hitJump = _hitJump;
    }

    public virtual void SetUpKnockBackDir(Transform _damage)
    {
        if (_damage.position.x > transform.position.x) knocBackDir = -1;
        else knocBackDir = 1;
    }

    protected virtual void SetUpZeroKnockBack()
    {

    }

    #region LayerCheck
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallDistance * faceDir, wallCheck.position.y));

        Gizmos.DrawWireSphere(attackCheck.position, attackR);
    }

    public bool GroundCheck() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, layer);

    public bool WallCheck() => Physics2D.Raycast(wallCheck.position, Vector2.right * faceDir, wallDistance, layer);

    #endregion

    #region Flip
    public void Flip()
    {
        faceDir = -faceDir;
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);


        onFlip?.Invoke();
    }

    public void UpdateFaceDirection(float inputX)
    {
        // 只有输入有效时才更新朝向，避免无输入时误翻转
        if (inputX != 0)
        {
            bool needFlip = (inputX > 0 && !faceRight) || (inputX < 0 && faceRight);
            if (needFlip)
            {
                Flip();
            }
        }
    }

    //public void FlipController(float _x)
    //{
    //    if (_x > 0 && !faceRight) Flip();
    //    else if (_x < 0 && faceRight) Flip();
    //}
    #endregion

    #region Ve
    public void SetVe(float _x, float _y)
    {
        rb.velocity = new Vector2(_x, _y); UpdateFaceDirection(_x);

    }
    #endregion

}
