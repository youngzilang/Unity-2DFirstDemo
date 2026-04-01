using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashSkill : Skill
{
    [Header("冲刺")]
    public bool dashUnlock;
    [SerializeField] SkillSlotUI dashUnlockButton;

    [Header("冲刺时克隆")]
    public bool cloneDashUnlock;
    [SerializeField] SkillSlotUI cloneDashUnlockButton;

    [Header("冲刺完成时克隆")]
    public bool cloneDashArriveUnlock;
    [SerializeField] SkillSlotUI cloneDashArriveUnlockButton;

    protected override void Start()
    {
        dashUnlockButton.GetComponent<Button>().onClick.AddListener(unlockDash);
        cloneDashUnlockButton.GetComponent<Button>().onClick.AddListener(unlockcloneDash);
        cloneDashArriveUnlockButton.GetComponent<Button>().onClick.AddListener(unlockcloneDashArrive);
    }
   
    public override bool CanSkill()
    {
        return base.CanSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
    }

    private void unlockDash()
    {
        if(dashUnlockButton.unlocked)
        {
            dashUnlock = true;
        }
    }

    private void unlockcloneDash()
    {
        if(cloneDashUnlockButton.unlocked)
        cloneDashUnlock = true;
    }

    private void unlockcloneDashArrive()
    {
        if(cloneDashArriveUnlockButton.unlocked)
        cloneDashArriveUnlock = true;
    }


    public void CloneOnDash(Transform clonePosition)
    {
        if (cloneDashUnlock)
            SkillManager.instance.cloneSkill.ClonePrefab(clonePosition, 0);
    }

    public void CloneOnDashArrival(Transform clonePosition)
    {
        if (cloneDashArriveUnlock)
            SkillManager.instance.cloneSkill.ClonePrefab(clonePosition, 0);
    }
}
