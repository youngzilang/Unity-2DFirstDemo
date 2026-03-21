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
    [SerializeField] private float balckHoleCd;

    private BlackHoleSkillController controller;
    public override bool CanSkill()
    {
        return base.CanSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject infectBlackHole = Instantiate(blackHolePrefab,player.transform.position+new Vector3(0,5),Quaternion.identity);
        controller= infectBlackHole.GetComponent<BlackHoleSkillController>();
        controller.SetUpBlackHole(maxSize, growSpeed, smallerSpeed, cloneAttackCd, cloneAttackAmount,balckHoleCd);
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool BlackHoleFinish()
    {
        if (!controller.playerExit) return false;

        if (controller.playerExit)
        {
            controller = null;
            return true;
        }
        return false;
    }

    public float R()
    {
        return maxSize / 2;
    }
}
