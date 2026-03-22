using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharaterStats
{
   

    protected override void Start()
    {
        base.Start();
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
    }
}
