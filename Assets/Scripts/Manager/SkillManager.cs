using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public Skill skill;

    public DashSkill dashSkill { get; private set; }
    public CloneSkill cloneSkill { get; private set; }

    public SwordSkill swordSkill { get; protected set; }

    public BlackHoleSkill blackHoleSkill { get; private set; }

    public CrystalSkill crystalSkill { get; private set; }

    public ParrySkill parrySkill { get; private set; }
    public DodgeSkill dodgeSkill { get; private set; }
    private void Awake()
    {
        if (instance != null&&instance!=this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    

    private void Start()
    {
        dashSkill = instance.GetComponent<DashSkill>();
        cloneSkill = instance.GetComponent<CloneSkill>();
        swordSkill = instance.GetComponent<SwordSkill>();
        blackHoleSkill = instance.GetComponent<BlackHoleSkill>();
        crystalSkill = instance.GetComponent<CrystalSkill>();
        parrySkill = instance.GetComponent<ParrySkill>();
        dodgeSkill = instance.GetComponent<DodgeSkill>();
    }
}
