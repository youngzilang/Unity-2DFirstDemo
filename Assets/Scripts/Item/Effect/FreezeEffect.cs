using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Freeze Effect",menuName ="Data/Item Effect/Freeze")]
public class FreezeEffect : ItemEffect
{
    [SerializeField] private float freezeContinue;

    public override void ExcuteEffect(Transform playerPosition)
    {
        PlayerStat playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();

        if (playerStat.currentHP < playerStat.maxHP.GetValue() * 0.1) return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPosition.position, 2);

        if (!Inventory.instance.UseArmor()) return;

        foreach(var hit in colliders)
        {
            hit.GetComponent<Enemy>()?.FreezeEffect(freezeContinue);
        }
    }

}
