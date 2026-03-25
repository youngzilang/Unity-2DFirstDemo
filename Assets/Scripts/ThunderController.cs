using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : MonoBehaviour
{
    private CharaterStats charaterStats;
    [SerializeField] private float speed;

    private Animator animator;
    private bool isTrigger;

    private int damage;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!charaterStats) return;

        if (isTrigger) return;

        transform.position = Vector2.MoveTowards(transform.position, charaterStats.transform.position, speed * Time.deltaTime);

        transform.right = transform.position - charaterStats.transform.position;

        if (Vector2.Distance(transform.position, charaterStats.transform.position) < .1f)
        {
            isTrigger = true;
            animator.transform.localRotation = Quaternion.identity;
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(3, 3);

            Invoke("ThunderDamage", .2f);
            animator.SetTrigger("Hit");
        }
    }

    public void SetUpThunder(int _damage,CharaterStats _stats)
    {
        damage = _damage;
        charaterStats = _stats;
    }

    private void ThunderDamage()
    {
        charaterStats.ApplyThunder(true);
        charaterStats.BeDamaged(damage);
        Destroy(transform.gameObject, .4f);
    }
}
