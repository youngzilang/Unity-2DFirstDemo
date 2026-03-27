using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderEffectController : EffectController
{

    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            EnemyStat enemyStat = collision.GetComponent<EnemyStat>();

            playerStat.DoingMagicDamage(enemyStat);
        }
    }
}
