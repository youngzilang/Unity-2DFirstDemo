using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharaterStats
{
    private Enemy enemy;
    private ItemDrop myDrop;
    public Stat soulDropAmount;

    [Header("等级信息")]
    [SerializeField] private int level=1;

    [Range(0, 1)]
    [SerializeField] private float percentagePlus=.4f;



    protected override void Start()
    {
        soulDropAmount.SetDefaultValue(100);
        LevelModify();

        base.Start();
        enemy = GetComponent<Enemy>();
        myDrop = GetComponent<ItemDrop>();
    }

    private void LevelModify()
    {
        Modify(defence);
        Modify(magicResistance);
        Modify(damage);
        Modify(maxHP);
        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightDamage);
        Modify(soulDropAmount);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void Modify(Stat stat)
    {
        for(int i = 1; i < level; i++)
        {
            float modify = stat.GetValue() * percentagePlus;
            stat.AddModify(Mathf.RoundToInt(modify));
        }
    }

    public override void BeDamaged(int _damage)
    {
        base.BeDamaged(_damage);
    }

    public override void DoingDamage(CharaterStats _stats)
    {
        base.DoingDamage(_stats);
    }

    public override void Die()
    {
        base.Die();
        enemy.Die();
        PlayerManager.instance.currency += soulDropAmount.GetValue();
        myDrop.GenerateDropObject();
    }
}
