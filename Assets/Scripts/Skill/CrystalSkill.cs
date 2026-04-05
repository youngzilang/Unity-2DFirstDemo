using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalSkill : Skill
{
    [SerializeField]private GameObject crystalPrefab;
    [SerializeField] private float crystalCd;
    [SerializeField] private float growSpeed;
    [SerializeField] private float moveSpeed;
    private GameObject currentCtystal;

    [Header("水晶数据")]
    [SerializeField] private float crystalAmount;
    [SerializeField] private float crystalAttackCd;
    [SerializeField] private float skillUseWindow;
    [SerializeField] private List<GameObject> crystalsList= new List<GameObject>();

    [Header("技能解锁")]
    [SerializeField] private SkillSlotUI crystalUnlockButton;
    public bool crystalUnlock { get; private set; }

    [SerializeField] private SkillSlotUI cloneInsteadCrystalUnlockButton;
    public bool cloneInsteadCrystalUnlock { get; private set; }

    [SerializeField] private SkillSlotUI exploseUnlockButton;
    public bool exploseUnlock { get; private set; }

    [SerializeField] private SkillSlotUI crystalMoveUnlockButton;
    public bool crystalMoveUnlock { get; private set; }

    [SerializeField] private SkillSlotUI multiCrystalUnlockButton;
    public bool multiCrystalUnlock { get; private set; }

    protected override void Start()
    {
        base.Start();

        crystalUnlockButton.GetComponent<Button>().onClick.AddListener(CrystalUnlock);
        cloneInsteadCrystalUnlockButton.GetComponent<Button>().onClick.AddListener(CloneInsteadCrystalUnlock);
        exploseUnlockButton.GetComponent<Button>().onClick.AddListener(ExploseUnlock);
        crystalMoveUnlockButton.GetComponent<Button>().onClick.AddListener(CrystalMoveUnlock);
        multiCrystalUnlockButton.GetComponent<Button>().onClick.AddListener(MultiCrystalUnlock);
    }


    protected override void CheckUnlock()
    {
        CrystalUnlock();
        CloneInsteadCrystalUnlock();
        ExploseUnlock();
        CrystalMoveUnlock();
        MultiCrystalUnlock();
    }
    public override void UseSkill()
    {
        base.UseSkill();

        if(CanUseCrystal())return;

        SingleCrystalAttack();

    }

    private void SingleCrystalAttack()
    {
        if (!currentCtystal)
        {
            CreatCrystal();
        }
        else
        {
            Vector2 originalPosition = player.transform.position;
            CrystalSkillController crystalSkillController = currentCtystal.GetComponent<CrystalSkillController>();
            player.transform.position = currentCtystal.transform.position;
            currentCtystal.transform.position = originalPosition;

            if (cloneInsteadCrystalUnlock)
            {
                player.skillManager.cloneSkill.ClonePrefab(currentCtystal.transform,0);
                Destroy(currentCtystal);
            }
            else
            crystalSkillController.Boom();
        }
    }

    public void RandomChooseTarget() => currentCtystal.GetComponent<CrystalSkillController>().RandomCrystalAttack();

    public void CreatCrystal()
    {
        currentCtystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
        currentCtystal.GetComponent<CrystalSkillController>().SetUpCrystal(crystalCd, growSpeed, moveSpeed);

        
    }

    public bool CanUseCrystal()
    {
        if (multiCrystalUnlock)
        {
            if (crystalsList.Count > 0)
            {
                if (crystalsList.Count == crystalAmount)
                    Invoke("FillCrystal", skillUseWindow);

                cd = 0;
                GameObject modelCrystal = crystalsList[crystalsList.Count - 1];
                GameObject newCrystal = Instantiate(modelCrystal,player.transform.position,Quaternion.identity);

                crystalsList.Remove(modelCrystal);
                newCrystal.GetComponent<CrystalSkillController>()?.SetUpCrystal(crystalCd, growSpeed, moveSpeed);

                if (crystalsList.Count <= 0)
                {
                    cd = crystalAttackCd;
                    AddCrystalAttackList();
                }
            }

            return true;
        }
        return false;
    }

    private void AddCrystalAttackList()
    {
        while (crystalsList.Count < crystalAmount)
        { crystalsList.Add(crystalPrefab); }
        
    }

    public override bool CanSkill()
    {
        return base.CanSkill();
    }

    private void FillCrystal()
    {
        if (cdTimer > 0) return;

        cdTimer = crystalAttackCd;
        AddCrystalAttackList();
    }

    private void CrystalUnlock()
    {
        if (crystalUnlockButton.unlocked) crystalUnlock = true;
    }

    private void CloneInsteadCrystalUnlock()
    {
        if (cloneInsteadCrystalUnlockButton.unlocked) cloneInsteadCrystalUnlock = true;
    }

    private void ExploseUnlock()
    {
        if (exploseUnlockButton.unlocked) exploseUnlock = true;
    }

    private void CrystalMoveUnlock()
    {
        if (crystalMoveUnlockButton.unlocked) crystalMoveUnlock = true;
    }

    private void MultiCrystalUnlock()
    {
        if (multiCrystalUnlockButton.unlocked) multiCrystalUnlock = true;
    }
}
