using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkillController : MonoBehaviour
{
    private Animator animator => GetComponent<Animator>();
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();

    private float crystalTimer;
    private bool isBoom = true;
    private float growSpeed;
    private float moveSpeed;
    private bool isGrow;
    private bool isMove=true;

    private void Update()
    {
        crystalTimer -= Time.deltaTime;

        Boom();

        if (isGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3,3), growSpeed * Time.deltaTime);
        }

        if (isMove)
        {
            if (FollowClosestEnemy())
            {
                transform.position = Vector2.MoveTowards(transform.position, FollowClosestEnemy().position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, FollowClosestEnemy().position) < 1)
                {
                    Boom();
                    isMove = false;
                }
            }
            
        }
    }

    public void Boom()
    {
        if (crystalTimer < 0)
        {
            if (isBoom)
            {
                isGrow = true;
                isMove = false;
                animator.SetTrigger("Boom");
            }
            else selfDestroy();
        }
    }

    public void SetUpCrystal(float _crystalCd, float _growSpeed,float _moveSpeed)
    {
        growSpeed = _growSpeed;
        crystalTimer = _crystalCd;
        moveSpeed = _moveSpeed;
    }

    private void selfDestroy(){
        isGrow = false;
        Destroy(gameObject);
    }
        
        

    private void BoomDamageTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,cd.radius);

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                collider.GetComponent<Enemy>().Damage();
            }
        }
    }

    private Transform FollowClosestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        Transform closestEnemy=null;

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
        }
        return closestEnemy;
    }
}
