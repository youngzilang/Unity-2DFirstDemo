using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : Skill
{
    [SerializeField]private GameObject crystalPrefab;
    [SerializeField] private float crystalCd;
    [SerializeField] private float growSpeed;
    [SerializeField] private float moveSpeed;
    private GameObject currentCtystal;

    [Header("Ë®¾§Êý¾Ý")]
    [SerializeField] private float crystalAmount;
    [SerializeField] private float crystalAttackCd;
    [SerializeField] private float skillUseWindow;
    [SerializeField] private List<GameObject> crystalsList= new List<GameObject>();
    [SerializeField] private bool isCrystalAttack;
    [SerializeField] private bool cloneInsteadCrystal;

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

            if (cloneInsteadCrystal)
            {
                player.skillManager.cloneSkill.ClonePrefab(currentCtystal.transform,0);
                Destroy(currentCtystal);
            }
            else
            crystalSkillController.Boom();
        }
    }

    public void CreatCrystal()
    {
        currentCtystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
        currentCtystal.GetComponent<CrystalSkillController>().SetUpCrystal(crystalCd, growSpeed, moveSpeed);
    }

    public bool CanUseCrystal()
    {
        if (isCrystalAttack)
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
}
