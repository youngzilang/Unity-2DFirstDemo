using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private SkillSlotUI blackHoleUnlockButton;
    public bool blackHoleUnlock { get; private set; }



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

    protected override void Start()
    {
        base.Start();

        blackHoleUnlockButton.GetComponent<Button>().onClick.AddListener(BlackHoleUnlock);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void CheckUnlock()
    {
        BlackHoleUnlock();
    }

    public bool BlackHoleFinish()
    {
        if (!controller.playerExit) return false;

        if (controller.playerExit)
        {
            //controller = null;
            return true;
        }
        return false;
    }

    public float R()
    {
        return maxSize / 2;
    }

    private void BlackHoleUnlock()
    {
        if (blackHoleUnlockButton.unlocked) blackHoleUnlock = true;
    }

}
