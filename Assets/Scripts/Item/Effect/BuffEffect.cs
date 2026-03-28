using UnityEngine;
enum buffType
{
    strength,
    intelligence,
    agility,
    vatility,
    damage,
    criticalChance,
    criticalDamage,
    maxHP,
    defence,
    magicResistance,
    evasion,
    fireDamage,
    iceDamage,
    lightDamage
}

[CreateAssetMenu(fileName = "Buff Effect", menuName = "Data/Item Effect/Buff")]
public class BuffEffect : ItemEffect
{
    [SerializeField] private buffType buff;
    [SerializeField] private int buffAmount;
    [SerializeField] private float buffContinueTime;

    private PlayerStat stat;

    public override void ExcuteEffect(Transform enemyPosition)
    {
         stat = PlayerManager.instance.player.GetComponent<PlayerStat>();
        stat.IncreaseBuff(buffAmount, buffContinueTime,SelectBuff() );

    }

    private Stat SelectBuff()
    {
        switch (buff)
        {
            case buffType.strength:return stat.strength;
            case buffType.intelligence:return stat.intelligence;
            case buffType.agility:return stat.agility;
            case buffType.vatility:return stat.vatility;
            case buffType.damage:return stat.damage;
            case buffType.criticalChance:return stat.criticalChance;
            case buffType.criticalDamage:return stat.criticalDamage;
            case buffType.maxHP:return stat.maxHP;
            case buffType.defence:return stat.defence;
            case buffType.magicResistance:return stat.magicResistance;
            case buffType.evasion:return stat.evasion;
            case buffType.fireDamage:return stat.fireDamage;
            case buffType.iceDamage:return stat.iceDamage;
            case buffType.lightDamage:return stat.lightDamage;
        }
        return null;
    }
}
