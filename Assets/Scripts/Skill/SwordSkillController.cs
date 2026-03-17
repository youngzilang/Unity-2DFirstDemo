using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class SwordSkillController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private CircleCollider2D collider2D;
    private Player player;
    [SerializeField] private float returnSpeed;
    private bool isRotate=true;
    private bool isReturn;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if(isRotate)
        transform.right = rb.velocity;

        if (isReturn)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,returnSpeed*Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                SkillManager.instance.swordSkill.DestroyMoreSword();
            }
        }
    }

    public void SwordReturn()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        isReturn = true;
    }

    public void SetUpSword(Vector2 direction,float g,Player player)
    {
        this.player = player;
        rb.velocity = direction;
        rb.gravityScale = g;

        animator.SetBool("isFlip", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturn) return;
        animator.SetBool("isFlip", false);
        isRotate = false;
        collider2D.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;
    }
}
