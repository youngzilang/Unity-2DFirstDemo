using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharaterStats
{
    private Enemy enemy;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void BeDamaged(int _damage)
    {
        base.BeDamaged(_damage);
        enemy.Damage();
    }

    public override void DoingDamage(CharaterStats _stats)
    {
        base.DoingDamage(_stats);
    }

    public override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
