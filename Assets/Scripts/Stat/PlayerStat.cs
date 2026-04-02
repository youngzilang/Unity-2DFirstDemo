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
    }

    public override void DoingDamage(CharaterStats _stats)
    {
        base.DoingDamage(_stats);
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.player.Die();
        GetComponent<PlayerItemDrop>()?.GenerateDropObject();
    }

    public override void DecreaseHp(int _damage)
    {
        base.DecreaseHp(_damage);

        ItemDataEquipment armor = Inventory.instance.GetEquipmentByType(EquipmentType.Armor);
        if (armor) armor.UseItemEffect(PlayerManager.instance.player.transform);
    }

    public override void OnEvasion()
    {
        SkillManager.instance.dodgeSkill.CloneOnDodge();
    }

    public void CloneDoDamage(CharaterStats _stats,float _percentage)
    {
        int total = damage.GetValue() + strength.GetValue();
        if (_percentage > 0) total = Mathf.RoundToInt(total * _percentage);

        if (CriticalOrNot()) total = CalculateCriticalDamage(total);

        total = CountDamageAfterDefence(total, _stats);
        _stats.BeDamaged(total);

        DoingMagicDamage(_stats);
    }
}
