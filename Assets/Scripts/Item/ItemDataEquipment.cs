using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,//武器
    Armor,//盔甲
    Amulet,//护符
    Flask//药瓶
}


[CreateAssetMenu(fileName = "Item Data", menuName = "Data/EquipMent")]
public class ItemDataEquipment : ItemData
{
    public EquipmentType equipmentType;
    public float armorCd;
    public float flaskCd;

    public ItemEffect[] itemEffects;

    [Header("主面板")]
    public int strength;//体力值：+1 点伤害，+1% 暴击伤害
    public int intelligence;//智力值：+1 魔法伤害，+ 1%暴击率
    public int agility;//敏捷值：+1%闪避率
    public int vatility;//活力值: +5生命

    [Header("攻击性属性")]
    public int damage;//攻击力
    public int criticalChance;//暴击率
    public int criticalDamage;//暴击伤害

    [Header("防御性属性")]
    public int maxHP;//最大血量
    public int defence;//防御力
    public int magicResistance;//法抗
    public int evasion;//闪避值


    [Header("法伤类属性")]
    public int fireDamage;//火属性
    public int iceDamage;//冰
    public int lightDamage;//光

    [Header("工艺图纸")]
    public List<InventoryItem> craftMaterial;

    public int descriptionLine;

    public void AddModify()
    {
        PlayerStat playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();
        playerStat.strength.AddModify(strength);
        playerStat.intelligence.AddModify(intelligence);
        playerStat.agility.AddModify(agility);
        playerStat.vatility.AddModify(vatility);
        playerStat.damage.AddModify(damage);
        playerStat.criticalChance.AddModify(criticalChance);
        playerStat.criticalDamage.AddModify(criticalDamage);
        playerStat.maxHP.AddModify(maxHP);
        playerStat.defence.AddModify(defence);
        playerStat.magicResistance.AddModify(magicResistance);
        playerStat.evasion.AddModify(evasion);
        playerStat.fireDamage.AddModify(fireDamage);
        playerStat.iceDamage.AddModify(iceDamage);
        playerStat.lightDamage.AddModify(lightDamage);
    }

    public void RemoveModify()
    {
        PlayerStat playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();
        playerStat.strength.RemoveModify(strength);
        playerStat.intelligence.RemoveModify(intelligence);
        playerStat.agility.RemoveModify(agility);
        playerStat.vatility.RemoveModify(vatility);
        playerStat.damage.RemoveModify(damage);
        playerStat.criticalChance.RemoveModify(criticalChance);
        playerStat.criticalDamage.RemoveModify(criticalDamage);
        playerStat.maxHP.RemoveModify(maxHP);
        playerStat.defence.RemoveModify(defence);
        playerStat.magicResistance.RemoveModify(magicResistance);
        playerStat.evasion.RemoveModify(evasion);
        playerStat.fireDamage.RemoveModify(fireDamage);
        playerStat.iceDamage.RemoveModify(iceDamage);
        playerStat.lightDamage.RemoveModify(lightDamage);
    }

    public void UseItemEffect(Transform enemyPosition)
    {
        foreach(var effect in itemEffects)
        {
            effect.ExcuteEffect(enemyPosition);
        }
    }

    public override string Description()
    {
        sb.Length = 0;
        descriptionLine = 0;

        AddDescription("体力", strength);
        AddDescription("智力", intelligence);
        AddDescription("敏捷", agility);
        AddDescription("活力", vatility);

        AddDescription("攻击力", damage);
        AddDescription("暴击率", criticalChance);
        AddDescription("暴击伤害", criticalDamage);

        AddDescription("最大生命值", maxHP);
        AddDescription("防御力", defence);
        AddDescription("魔抗", magicResistance);
        AddDescription("闪避", evasion);

        AddDescription("火属性", fireDamage);
        AddDescription("冰属性", iceDamage);
        AddDescription("雷属性", lightDamage);

        if (descriptionLine < 4)
        {
            for(int i = 0; i < 4 - descriptionLine; i++)
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }

    public void AddDescription(string _name,int value)
    {
        if (value != 0)
        {
            if (sb.Length > 0) sb.AppendLine();

            if (value > 0) sb.Append("+ "+value+" "+_name);

            descriptionLine++;
        }
    }
}
