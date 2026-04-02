using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSkill : Skill
{
    [Header("克隆技能信息")]
    [SerializeField]private GameObject clonePrefab;
    [SerializeField] private float cloneCd;
    [SerializeField] private bool canCloneAttack;
    [SerializeField] private float addCloneChance;
    [SerializeField] private bool canAddClone;

    [Header("技能解锁")]
    [SerializeField] private SkillSlotUI cloneAttackUnlockButton;
    [SerializeField] private float cloneAttackPercentage;
    public bool cloneAttackUnlock { get; private set; }

    [SerializeField] private SkillSlotUI aggresiveCloneUnlockButton;
    [SerializeField] private float aggresiveCloneAttackPercentage;
    public bool applyOnHitEffect { get; private set; }

    [SerializeField] private SkillSlotUI mutipleCloneUnlockButton;
    [SerializeField] private float mutipleClonePercentage;
    public bool mutipleCloneUnlock { get; private set; }

    [SerializeField] private SkillSlotUI crystalInsteadCloneUnlockButton;
    public bool crystalInsteadCloneUnlock { get; private set; }

    private float attackPercentage;

    protected override void Start()
    {
        base.Start();

        cloneAttackUnlockButton.GetComponent<Button>().onClick.AddListener(CloneAttackUnlock);
        aggresiveCloneUnlockButton.GetComponent<Button>().onClick.AddListener(AggresiveCloneUnlock);
        mutipleCloneUnlockButton.GetComponent<Button>().onClick.AddListener(MutipleCloneUnlock);
        crystalInsteadCloneUnlockButton.GetComponent<Button>().onClick.AddListener(CrystalInsteadCloneUnlock);
    }




    #region Unlock

    private void CloneAttackUnlock()
    {
        if (cloneAttackUnlockButton.unlocked)
        {
            cloneAttackUnlock = true;
            attackPercentage = cloneAttackPercentage;
        }
    }

    private void AggresiveCloneUnlock()
    {
        if (aggresiveCloneUnlockButton.unlocked)
        {
            applyOnHitEffect = true;
            attackPercentage = aggresiveCloneAttackPercentage;
        }
    }
    private void MutipleCloneUnlock()
    {
        if (mutipleCloneUnlockButton.unlocked)
        {
            mutipleCloneUnlock = true;
            attackPercentage = mutipleClonePercentage;
        }
    }

    private void CrystalInsteadCloneUnlock()
    {
        if (crystalInsteadCloneUnlockButton.unlocked) crystalInsteadCloneUnlock = true;
    }

    #endregion



    public void ClonePrefab(Transform clonePosition, int xOffSet)
    {
        if (crystalInsteadCloneUnlock)
        {
            SkillManager.instance.crystalSkill.CreatCrystal();
            return;
        }

        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<CloneSkillController>().SetUpClone(clonePosition, cloneCd, cloneAttackUnlock, xOffSet, mutipleCloneUnlock, addCloneChance,attackPercentage);
    }

    public void DelayCreatReAttackClone(Transform _transform,int _offset)
    {
      StartCoroutine(Delay(_transform, _offset));
        
    }

    private IEnumerator Delay(Transform _transform, int _offset)
    {
        yield return new WaitForSeconds(.4f);
        ClonePrefab(_transform, _offset);
    }
}
