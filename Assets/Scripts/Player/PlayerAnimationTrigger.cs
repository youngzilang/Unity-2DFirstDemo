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
                EnemyStat target = collider.GetComponent<EnemyStat>();
                player.stats.DoingDamage(target);
                // player.stats.DoingDamage(target);
                // collider.GetComponent<Enemy>().Damage();
                // collider.GetComponent<CharaterStats>().BeDamaged(player.stats.damage.GetValue());
                Inventory.instance.GetEquipmentByType(EquipmentType.Weapon)?.UseItemEffect(target.transform);
            }
        }
    }

    private void ThrowSword()
    {
        SkillManager.instance.swordSkill.CreatSword();
    }
}
