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

    private Transform closestTarget;
    [SerializeField] private LayerMask theEnemy;
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
            if (closestTarget)
            {
                transform.position = Vector2.MoveTowards(transform.position, closestTarget.position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, closestTarget.position) < 1)
                {
                    Boom();
                    isMove = false;
                }
            }
            
        }
    }

    public void RandomCrystalAttack()
    {
        float r = SkillManager.instance.blackHoleSkill.R();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, r,theEnemy);

        if(colliders.Length>0)
        closestTarget = colliders[Random.Range(0, colliders.Length)].transform;
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

                PlayerManager.instance.player.stats.DoingMagicDamage(collider.GetComponent<CharaterStats>());
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
