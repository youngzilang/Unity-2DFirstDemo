using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class BlackHoleSkill : Skill
{
    [Header("ºÚ¶´ÐÅÏ¢")]
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField]private float smallerSpeed;
    [SerializeField] private GameObject blackHolePrefab;
    [SerializeField] private int cloneAttackAmount;
    [SerializeField] private float cloneAttackCd;

    public override bool CanSkill()
    {
        return base.CanSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject infectBlackHole = Instantiate(blackHolePrefab,player.transform.position,Quaternion.identity);
        infectBlackHole.GetComponent<BlackHoleSkillController>().SetUpBlackHole(maxSize, growSpeed, smallerSpeed, cloneAttackCd, cloneAttackAmount);
    }

    protected override void Update()
    {
        base.Update();
    }
}
