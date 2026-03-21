using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    } 

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, player.attackR);

        foreach(var collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                collider.GetComponent<Enemy>().Damage();
                collider.GetComponent<CharaterStats>().BeDamaged(player.stats.damage.GetValue());
            }
        }
    }

    private void ThrowSword()
    {
        SkillManager.instance.swordSkill.CreatSword();
    }
}
