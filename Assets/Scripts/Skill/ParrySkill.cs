using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParrySkill : Skill
{
    [SerializeField] private SkillSlotUI parryUnlockButton;
    public bool parryUnlock { get; private set; }

    [SerializeField] private SkillSlotUI restoreUnlockButton;
    public bool restoreUnlock{ get; private set; }

    [SerializeField] private SkillSlotUI parryWithCloneUnlockButton;
    public bool parryWithCloneUnlock{ get; private set; }

    [Header("ÑªÁ¿»Ö¸´±ÈÀý")]
    [Range(0,1)]
    [SerializeField] private float restoreHpPercentage;

    protected override void Start()
    {
        base.Start();

        parryUnlockButton.GetComponent<Button>().onClick.AddListener(ParryUnlock);
        restoreUnlockButton.GetComponent<Button>().onClick.AddListener(RestoreUnlock);
        parryWithCloneUnlockButton.GetComponent<Button>().onClick.AddListener(ParryWithCloneUnlock);
    }

    public override bool CanSkill()
    {
        return base.CanSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        if (restoreUnlock)
        {
            int restoreHp = Mathf.RoundToInt(player.stats.GetMaxHp() * restoreHpPercentage);
            player.stats.IncreaseHp(restoreHp);
        }
    }

    private void ParryUnlock()
    {
        if (parryUnlockButton.unlocked) parryUnlock = true;
    }

    private void RestoreUnlock()
    {
        if (restoreUnlockButton.unlocked) restoreUnlock = true;
    }

    private void ParryWithCloneUnlock()
    {
        if (parryWithCloneUnlockButton.unlocked) parryWithCloneUnlock = true;
    }

    public void CloneOnParry(Transform _transform, int _offset)
    {
        if (parryWithCloneUnlock) SkillManager.instance.cloneSkill.DelayCreatReAttackClone(_transform, _offset);
    }

}
