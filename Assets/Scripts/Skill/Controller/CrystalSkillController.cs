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
    private bool isGrow;

    private void Update()
    {
        crystalTimer -= Time.deltaTime;

        Boom();

        if (isGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3,3), growSpeed * Time.deltaTime);
        }
    }

    public void Boom()
    {
        if (crystalTimer < 0)
        {
            if (isBoom)
            {
                isGrow = true;
                animator.SetTrigger("Boom");
            }
            else selfDestroy();
        }
    }

    public void SetUpCrystal(float _crystalCd, float _growSpeed)
    {
        growSpeed = _growSpeed;
        crystalTimer = _crystalCd;
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
}
