using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal fect", menuName = "Data/Item Effect/Heal")]
public class HealEffect : ItemEffect
{
    [Range(0, 1)]
    [SerializeField] private float healPercent;

    public override void ExcuteEffect(Transform enemyPosition)
    {
        PlayerStat playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();

        int healAmount = Mathf.RoundToInt(playerStat.GetMaxHp() * healPercent);

        playerStat.IncreaseHp(healAmount);
    }
}
