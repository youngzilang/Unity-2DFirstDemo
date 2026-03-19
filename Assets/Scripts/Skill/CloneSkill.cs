using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("克隆技能信息")]
    [SerializeField]private GameObject clonePrefab;
    [SerializeField] private float cloneCd;
    [SerializeField] private bool canCloneAttack;
    


    public void ClonePrefab(Transform clonePosition, int xOffSet)
    {
        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<CloneSkillController>().SetUpClone(clonePosition, cloneCd,canCloneAttack,xOffSet);
    }
}
