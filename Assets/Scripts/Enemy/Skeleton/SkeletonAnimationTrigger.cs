using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();

    protected void AnimationTrigger()
    { 
        enemy.AnimationFinishTrigger();
    }

    protected void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.transform.position, enemy.attackR);

        foreach(var collider in colliders)
        {
            if (collider.GetComponent<Player>() != null)
            {
                PlayerStat target= collider.GetComponentInChildren<PlayerStat>();
                enemy.stats.DoingDamage(target);
                //skeleton.stats.DoingDamage(target);
                //collider.GetComponent<Player>().Damage();
            }
        }
    }

    private void OpenStunWindow() => enemy.OpenStunWindow();

    private void CloseStunWindow() => enemy.CloseStunWindow();
}
