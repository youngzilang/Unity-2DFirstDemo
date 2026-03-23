using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    private Enemy_Skeleton skeleton => GetComponentInParent<Enemy_Skeleton>();

    private void AnimationTrigger()
    {
        skeleton.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(skeleton.transform.position, skeleton.attackR);

        foreach(var collider in colliders)
        {
            if (collider.GetComponent<Player>() != null)
            {
                PlayerStat target= collider.GetComponentInChildren<PlayerStat>();
                skeleton.stats.DoingMagicDamage(target);
                //skeleton.stats.DoingDamage(target);
                //collider.GetComponent<Player>().Damage();
            }
        }
    }

    private void OpenStunWindow() => skeleton.OpenStunWindow();

    private void CloseStunWindow() => skeleton.CloseStunWindow();
}
