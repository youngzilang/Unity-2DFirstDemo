using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterStats : MonoBehaviour
{
    [Header("主面板")]
    public Stat strength;//体力值：+1 点伤害，+1% 暴击伤害
    public Stat intelligence;//智力值：+1 魔法伤害，+ 暴击率
    public Stat agility;//敏捷值：+1闪避率
    public Stat vatility;//活力值: 生命

    [Header("攻击性属性")]
    public Stat damage;//攻击力
    public Stat criticalChance;//暴击率
    public Stat criticalDamage;//暴击伤害

    [Header("防御性属性")]
    public Stat maxHP;//最大血量
    public Stat defence;//防御力
    public Stat evasion;//闪避值

    [SerializeField]private int currentHP;


    protected virtual void Start()
    {
        criticalDamage.SetDefaultValue(150);
        currentHP = maxHP.GetValue();

    }

    public virtual void DoingDamage(CharaterStats _stats)
    {

        int total = damage.GetValue() + strength.GetValue();

        total = total - _stats.defence.GetValue() < 0 ? 0 : total - _stats.defence.GetValue();

        _stats.BeDamaged(total);
    }

    public virtual void BeDamaged(int _damage)
    {
        if (EvasionSuccessOrNot()) return;

        currentHP -= _damage;

        if (currentHP <= 0) Die();
    }

    public virtual  void Die()
    {

    }

    public bool EvasionSuccessOrNot()
    {
        int totalEvasion = agility.GetValue() + evasion.GetValue();
        if (Random.Range(0, 100) < totalEvasion) return true;
        return false;
    }

}
