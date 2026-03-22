using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterStats : MonoBehaviour
{
    [Header("主面板")]
    public Stat strength;//体力值：+1 点伤害，+1% 暴击伤害
    public Stat intelligence;//智力值：+1 魔法伤害，+ 暴击率
    public Stat agility;//敏捷值：+1闪避率
    public Stat vatility;//活力值: +生命

    [Header("攻击性属性")]
    public Stat damage;//攻击力
    public Stat criticalChance;//暴击率
    public Stat criticalDamage;//暴击伤害

    [Header("防御性属性")]
    public Stat maxHP;//最大血量
    public Stat defence;//防御力
    public Stat evasion;//闪避值
    public Stat magicResistance;//法抗

    [Header("法伤类属性")]
    public Stat fireDamage;//火属性
    public bool isFire;
    public Stat iceDamage;//冰
    public bool isIce;
    public Stat lightDamage;//光
    public bool isLight;




    [SerializeField]private int currentHP;


    protected virtual void Start()
    {
        criticalDamage.SetDefaultValue(150);
        currentHP = maxHP.GetValue();

    }

    public virtual void DoingMagicDamage(CharaterStats _stats)
    {
        int total = fireDamage.GetValue() + iceDamage.GetValue() + lightDamage.GetValue()+intelligence.GetValue();
        total =total- magicResistance.GetValue()<0?0:total- magicResistance.GetValue();
        _stats.BeDamaged(total);
    }

    

    public virtual void DoingDamage(CharaterStats _stats)
    {

        int total = damage.GetValue() + strength.GetValue();

        if (CriticalOrNot()) total = CalculateCriticalDamage(total);

        total = CountDamageAfterDefence(total,_stats);
        _stats.BeDamaged(total);
    }

    public virtual void BeDamaged(int _damage)
    {
        if (EvasionSuccessOrNot()) return;

        currentHP -= _damage;

        if (currentHP <= 0) Die();
    }

    public void ApplyElement(bool _fire, bool _ice, bool _light)
    {
        if (isFire || isIce || isLight) return;

        isFire = _fire;
        isIce = _ice;
        isLight = _light;
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

    public bool CriticalOrNot()
    {
        if (Random.Range(0, 100) < criticalChance.GetValue()+intelligence.GetValue()) return true;
        return false;
    }

    public int CountDamageAfterDefence(int _damage, CharaterStats _stats)
    {
        return _damage - _stats.defence.GetValue() < 0 ? 0 : _damage - _stats.defence.GetValue();
    }

    public int CalculateCriticalDamage(int _damage)
    {
        float total = (strength.GetValue() + criticalDamage.GetValue()) * 0.01f;
        return Mathf.RoundToInt(total * _damage);
    }
}
