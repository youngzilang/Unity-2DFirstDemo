using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public enum buffType
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


public class CharaterStats : MonoBehaviour
{
    [Header("主面板")]
    public Stat strength;//体力值：+1 点伤害，+1% 暴击伤害
    public Stat intelligence;//智力值：+1 魔法伤害，+ 1%暴击率
    public Stat agility;//敏捷值：+1%闪避率
    public Stat vatility;//活力值: +5生命

    [Header("攻击性属性")]
    public Stat damage;//攻击力
    public Stat criticalChance;//暴击率
    public Stat criticalDamage;//暴击伤害

    [Header("防御性属性")]
    public Stat maxHP;//最大血量
    public Stat defence;//防御力
    public Stat magicResistance;//法抗
    public Stat evasion;//闪避值
    

    [Header("法伤类属性")]
    public Stat fireDamage;//火属性
    public Stat iceDamage;//冰
    public Stat lightDamage;//光


    [Header("负面效果")]
    public bool isFire;//被点燃持续造成伤害(敌方火属性的20%)
    public bool isIce;//护甲值降低20%
    public bool isLight;//闪避率降低20%(被闪电击中造成20%伤害)

    public int currentHP;

    private float fireTimer;
    private float fireDamageCd=.3f;
    private float fireDamageTimer;
    private int beBurnDamage;

    private float iceTimer;
    private float lightTimer;
    private float elementTimer=2;

    public Action onHPChange;

    private FX fX;
    private Entity entity;

    [SerializeField] private GameObject thunderPrefab;

    public bool isDead { get; private set; }

    private bool vulnerable;

    protected virtual void Start()
    {
        criticalDamage.SetDefaultValue(150);
        currentHP = GetMaxHp();
        fX = GetComponent<FX>();
        entity = GetComponent<Entity>();
    }

    protected virtual void Update()
    {
        fireTimer -= Time.deltaTime;
        fireDamageTimer -= Time.deltaTime;
        iceTimer -= Time.deltaTime;
        lightTimer -= Time.deltaTime;

        if (fireTimer < 0)
        {
            isFire = false;
        }

        if (iceTimer < 0)
        {
            isIce = false;
        }

        if(lightTimer < 0)
        {
            isLight = false;
        }

        if (fireDamageTimer < 0 && isFire)
        {
            DecreaseHp(beBurnDamage);
            if (currentHP <= 0&&!isDead) Die();
            fireDamageTimer = fireDamageCd;
        }
    }

    public virtual void IncreaseBuff(int _buff,float continueTime,Stat _toAddStat)
    {
        StartCoroutine(AddBuff(_buff,continueTime,_toAddStat));
    }

    public void MakeVulnerable(float _continue)=> StartCoroutine(VulnerableCoroutine(_continue));

    private IEnumerator VulnerableCoroutine(float _continue)
    {
        vulnerable = true;
        yield return new WaitForSeconds(_continue);
        vulnerable = false;
    }

    IEnumerator AddBuff(int _buff, float continueTime, Stat _toAddStat)
    {
        _toAddStat.AddModify(_buff);
        yield return new WaitForSeconds(continueTime);
        _toAddStat.RemoveModify(_buff);
    }

    //灼烧
    private void BeBurn(int _beBurnDamage)
    {
        beBurnDamage = _beBurnDamage;
    }

    //魔法攻击
    public virtual void DoingMagicDamage(CharaterStats _stats)
    {
        int total = fireDamage.GetValue() + iceDamage.GetValue() + lightDamage.GetValue()+intelligence.GetValue();
        total =total- magicResistance.GetValue()<0?0:total- magicResistance.GetValue();
        _stats.BeDamaged(total);

        if(Mathf.Max(fireDamage.GetValue(),iceDamage.GetValue(),lightDamage.GetValue())<=0)return;

        bool fireOrNot = fireDamage.GetValue() > iceDamage.GetValue() && fireDamage.GetValue() > lightDamage.GetValue();
        bool iceOrNot= iceDamage.GetValue() > fireDamage.GetValue() && iceDamage.GetValue() > lightDamage.GetValue();
        bool lightOrNot= lightDamage.GetValue() > iceDamage.GetValue() && lightDamage.GetValue() > fireDamage.GetValue();

        while (!fireOrNot && !iceOrNot && !lightOrNot)
        {
            int ran = UnityEngine.Random.Range(0, 100);
            if (ran < 33&&fireDamage.GetValue()>0)
            {
                fireOrNot = true;
                Debug.Log("Fire!");
                _stats.ApplyElement(fireOrNot, iceOrNot, lightOrNot);
                return;
            }
            else if (ran < 66&& iceDamage.GetValue()>0)
            {
                iceOrNot = true;
                Debug.Log("Ice!");
                _stats.ApplyElement(fireOrNot, iceOrNot, lightOrNot);
                return;
            }
            else if(ran<100 && lightDamage.GetValue()>0)
            {
                lightOrNot = true;
                Debug.Log("Light!");
                _stats.ApplyElement(fireOrNot, iceOrNot, lightOrNot);
                return;
            }
        }
        
        if(fireOrNot)
        _stats.BeBurn(Mathf.RoundToInt(fireDamage.GetValue() * .2f));


        _stats.ApplyElement(fireOrNot, iceOrNot, lightOrNot);
    }

    //物理攻击
    public virtual void DoingDamage(CharaterStats _stats)
    {

        int total = damage.GetValue() + strength.GetValue();

        if (CriticalOrNot()) total = CalculateCriticalDamage(total);

        total = CountDamageAfterDefence(total,_stats);
        _stats.BeDamaged(total);

        DoingMagicDamage(_stats);
    }

    //受击伤害判定
    public virtual void BeDamaged(int _damage)
    {
        if (EvasionSuccessOrNot()) return;

        DecreaseHp(_damage);

        GetComponent<Entity>()?.Damage();
        fX.StartCoroutine("Fx");

        if (currentHP <= 0&&!isDead) Die();
    }
    //负面效果判定
    public void ApplyElement(bool _fire, bool _ice, bool _light)
    {
        bool canFire = !isFire && !isIce && !isLight;
        bool canIce=!isFire && !isIce && !isLight;
        bool canLight = !isFire && !isIce;

        if (_fire&&canFire)
        {
            isFire = _fire;
            fireTimer = elementTimer;
            fX.FireFor(fireTimer);
        }
        if(_ice&&canIce)
        {
            isIce = _ice;
            iceTimer = elementTimer;
            entity.SlowByIce(.2f, iceTimer);
            fX.IceFor(iceTimer);
        }
        if (_light&&canLight)
        {

            if (!isLight)
            {
                ApplyThunder(_light);
            }
            else
            {
                if (GetComponent<Player>() != null) return;
                ThunderClosestTarget();
            }
        }
        
    }

    public void ApplyThunder(bool _light)
    {
        if (isLight) return;
        isLight = _light;
        lightTimer = elementTimer;
        fX.LightFor(lightTimer);
    }

    private void ThunderClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        Transform cloestEnemy = null;
        float maxDistance = Mathf.Infinity;
        foreach (var item in colliders)
        {
            if (item.GetComponent<Enemy>() != null)
            {
                if (item.GetComponent<Enemy>() == GetComponent<Enemy>()) continue;
                if (Vector2.Distance(transform.position, item.GetComponent<Enemy>().transform.position) < maxDistance)
                {
                    maxDistance = Vector2.Distance(transform.position, item.GetComponent<Enemy>().transform.position);
                    cloestEnemy = item.GetComponent<Enemy>().transform;
                }
            }
        }
        if (cloestEnemy == null) cloestEnemy = transform;

        if (cloestEnemy != null)
        {
            GameObject newThunder = Instantiate(thunderPrefab, transform.position, Quaternion.identity);

            newThunder.GetComponent<ThunderController>().SetUpThunder(Mathf.RoundToInt(lightDamage.GetValue()*.2f), cloestEnemy.GetComponent<CharaterStats>());
        }
    }

    //死亡
    public virtual  void Die()
    {
        isDead = true;
    }
    

    public virtual void OnEvasion()
    {

    }

    //闪避判断
    public bool EvasionSuccessOrNot()
    {
        int totalEvasion = agility.GetValue() + evasion.GetValue();
        if (isLight) totalEvasion = totalEvasion - 20 < 0 ? 0 : totalEvasion - 20;

        if (UnityEngine.Random.Range(0, 100) < totalEvasion)
        {
            OnEvasion();
            return true;
        }
        return false;
    }
    //暴击判断
    public bool CriticalOrNot()
    {
        if (UnityEngine.Random.Range(0, 100) < criticalChance.GetValue()+intelligence.GetValue()) return true;
        return false;
    }

    //穿甲
    public int CountDamageAfterDefence(int _damage, CharaterStats _stats)
    {
        if (isIce) _damage = Mathf.RoundToInt(_damage * .8f);

        return _damage - _stats.defence.GetValue() < 0 ? 0 : _damage - _stats.defence.GetValue();
    }

    //暴伤
    public int CalculateCriticalDamage(int _damage)
    {
        float total = (strength.GetValue() + criticalDamage.GetValue()) * 0.01f;
        return Mathf.RoundToInt(total * _damage);
    }

    public virtual void DecreaseHp(int _damage)
    {
        if (vulnerable) _damage = Mathf.RoundToInt(_damage * 1.1f);

        currentHP -= _damage;
        if (onHPChange != null)
        {
            onHPChange();
        }
    }

    public virtual void IncreaseHp(int _heal)
    {
        currentHP += _heal;

        if (currentHP > GetMaxHp()) currentHP = GetMaxHp();

        if (onHPChange != null) onHPChange();
    }

    public int GetMaxHp()
    {
        return maxHP.GetValue() + vatility.GetValue() * 5;
    }

    public Stat SelectBuff(buffType buff)
    {
        switch (buff)
        {
            case buffType.strength: return strength;
            case buffType.intelligence: return intelligence;
            case buffType.agility: return agility;
            case buffType.vatility: return vatility;
            case buffType.damage: return damage;
            case buffType.criticalChance: return criticalChance;
            case buffType.criticalDamage: return criticalDamage;
            case buffType.maxHP: return maxHP;
            case buffType.defence: return defence;
            case buffType.magicResistance: return magicResistance;
            case buffType.evasion: return evasion;
            case buffType.fireDamage: return fireDamage;
            case buffType.iceDamage: return iceDamage;
            case buffType.lightDamage: return lightDamage;
        }
        return null;
    }
}
