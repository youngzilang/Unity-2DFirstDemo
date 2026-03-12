using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Åö×²¼ì²â")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float groundDistance;
    [SerializeField] protected float wallDistance;
    [SerializeField] protected LayerMask layer;


    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion


    public int faceDir { get; private set; } = 1;
    protected bool faceRight { get; private set; } = true;


    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }


    #region LayerCheck
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallDistance * faceDir, wallCheck.position.y));
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
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !faceRight) Flip();
        else if (_x < 0 && faceRight) Flip();
    }
    #endregion

    #region Ve
    public void SetVe(float _x, float _y)
    {
        rb.velocity = new Vector2(_x, _y); FlipController(_x);

    }
    #endregion

}
