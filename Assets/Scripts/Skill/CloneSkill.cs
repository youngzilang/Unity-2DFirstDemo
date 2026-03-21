using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("克隆技能信息")]
    [SerializeField]private GameObject clonePrefab;
    [SerializeField] private float cloneCd;
    [SerializeField] private bool canCloneAttack;
    [SerializeField] private float addCloneChance;
    [SerializeField] private bool dashStartClone;
    [SerializeField] private bool dashOverClone;
    [SerializeField] private bool cloneReAttack;
    [SerializeField] private bool canAddClone;
    [SerializeField] private bool crystalInsteadClone;

    public void ClonePrefab(Transform clonePosition, int xOffSet)
    {
        if (crystalInsteadClone)
        {
            SkillManager.instance.crystalSkill.CreatCrystal();
            return;
        }

        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<CloneSkillController>().SetUpClone(clonePosition, cloneCd,canCloneAttack,xOffSet,canAddClone,addCloneChance);
    }

    public void CreatDashStartClone(Transform clonePosition)
    {
        if (dashStartClone)
            ClonePrefab(clonePosition,0);
    }

    public void CreatDashOverClone(Transform clonePosition)
    {
        if (dashOverClone)
            ClonePrefab(clonePosition, 0);
    }

    public void DelayCreatReAttackClone(Transform _transform,int _offset)
    {
        if (cloneReAttack) StartCoroutine(Delay(_transform, _offset));
        
    }

    private IEnumerator Delay(Transform _transform, int _offset)
    {
        yield return new WaitForSeconds(.4f);
        ClonePrefab(_transform, _offset);
    }
}
