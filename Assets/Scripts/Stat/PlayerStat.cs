using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharaterStats
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void BeDamaged(int _damage)
    {
        base.BeDamaged(_damage);
        PlayerManager.instance.player.Damage();
    }

    public override void DoingDamage(CharaterStats _stats)
    {
        base.DoingDamage(_stats);
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.player.Die();
    }
}
