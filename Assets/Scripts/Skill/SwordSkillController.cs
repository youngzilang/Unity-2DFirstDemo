using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class SwordSkillController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rg;
    private CircleCollider2D collider2D;
    private Player player;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rg = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<CircleCollider2D>();
    }

    public void SetUpSword(Vector2 direction,float g)
    {
        rg.velocity = direction;
        rg.gravityScale = g;
    }

}
