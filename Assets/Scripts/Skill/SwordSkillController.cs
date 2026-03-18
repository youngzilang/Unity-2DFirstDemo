using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private CircleCollider2D collider2D;
    private Player player;
    [SerializeField] private float returnSpeed;
    private bool isRotate = true;
    private bool isReturn;

    [Header("·´µ¯Êý¾Ý")]
    [SerializeField]private float bounceSpeed;
    private bool isBounce;
    private int bounceAmount;
    private List<Transform> transforms;
    private int transformsIndex;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (isRotate)
            transform.right = rb.velocity;

        if (isReturn)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                SkillManager.instance.swordSkill.DestroyMoreSword();
            }
        }

        BounceLogic();
    }

    private void BounceLogic()
    {
        if (isBounce && transforms.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, transforms[transformsIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, transforms[transformsIndex].position) < .1f)
            {
                transformsIndex++;
                bounceAmount--;

                if (bounceAmount <= 0)
                {
                    isBounce = false;
                    isReturn = true;
                }

                if (transformsIndex >= transforms.Count)
                {
                    transformsIndex = 0;
                }
            }
        }
    }

    public void SetUpBounce(bool bounce,int _bounceAmount)
    {
        isBounce = bounce;
        bounceAmount = _bounceAmount;
        transforms = new List<Transform>();
    }

    public void SwordReturn()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        isReturn = true;
    }

    public void SetUpSword(Vector2 direction, float g, Player player)
    {
        this.player = player;
        rb.velocity = direction;
        rb.gravityScale = g;

        animator.SetBool("isFlip", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (isReturn) return;

        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBounce && transforms.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var a in colliders)
                {
                    if (a.GetComponent<Enemy>() != null)
                    {
                        transforms.Add(a.transform);
                    }
                }
            }
        }

        SwordStuck(collision);
    }

    private void SwordStuck(Collider2D collision)
    {
        isRotate = false;
        collider2D.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBounce && transforms.Count > 0) return;

        animator.SetBool("isFlip", false);
        transform.parent = collision.transform;
    }
}
