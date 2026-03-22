using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterStats : MonoBehaviour
{
    public Stat damage;
    public Stat strength;
    public Stat maxHP;

    [SerializeField]private int currentHP;


    protected virtual void Start()
    {
        currentHP = maxHP.GetValue();

    }

    public virtual void DoingDamage(CharaterStats _stats)
    {

        int total = damage.GetValue() + strength.GetValue();
        _stats.BeDamaged(total);
    }

    public virtual void BeDamaged(int _damage)
    {
        currentHP -= _damage;

        if (currentHP <= 0) Die();
    }

    public virtual  void Die()
    {

    }
}
