using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.UI;

public class DodgeSkill : Skill
{
    [SerializeField] private SkillSlotUI dodgeUnlockButton;
    [SerializeField] private int evasionAmount;
    private bool dodgeUnlock;

    [SerializeField] private SkillSlotUI dodgeCloneUnlockButton;
    private bool dodgeCloneUnlock;

    protected override void Start()
    {
        base.Start();

        dodgeUnlockButton.GetComponent<Button>().onClick.AddListener(DodgeUnlock);
        dodgeCloneUnlockButton.GetComponent<Button>().onClick.AddListener(DodgeCloneUnlock);
    }


    private void DodgeUnlock()
    {
        if (dodgeUnlockButton.unlocked)
        {
            player.stats.evasion.AddModify(evasionAmount);
            Inventory.instance.UpdateStatUI();
            dodgeUnlock = true;
        }
    }

    private void DodgeCloneUnlock()
    {
        if (dodgeCloneUnlockButton.unlocked) dodgeCloneUnlock = true;
    }

    public void CloneOnDodge()
    {
        if (dodgeCloneUnlock) SkillManager.instance.cloneSkill.ClonePrefab(player.transform, 2*player.faceDir);
    }
}
