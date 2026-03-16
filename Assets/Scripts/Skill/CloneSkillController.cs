using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class CloneSkillController : MonoBehaviour
{
    [SerializeField] private float cloneTransparentSpeed;
    [SerializeField] private Transform clone;
    [SerializeField] private float cloneAttackR;
    private SpriteRenderer sr;
    private Animator animator;
    private float cloneTimer;
    private Transform closestEnemy;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (cloneTransparentSpeed * Time.deltaTime));
        }

        if (sr.color.a < 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetUpClone(Transform clonePosition,float _cloneTimer,bool cloneAttack)
    {
        if(cloneAttack)
        {
            animator.SetInteger("attackNum", UnityEngine.Random.Range(1,4));
        }

        transform.position = clonePosition.position;
        cloneTimer = _cloneTimer;
        FaceEnemy();
    }

    private void AnimationTrigger()
    {
        cloneTimer = -1;
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(clone.position, cloneAttackR);

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                collider.GetComponent<Enemy>().Damage();
            }
        }
    }

    private void FaceEnemy()
    {
        closestEnemy = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {


                float distance = Vector2.Distance(transform.position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = collider.transform;

                }
            }

            if (closestEnemy != null)
            {
                if (transform.position.x > closestEnemy.position.x) transform.Rotate(0, 180, 0);
            }
        }
    }
}
