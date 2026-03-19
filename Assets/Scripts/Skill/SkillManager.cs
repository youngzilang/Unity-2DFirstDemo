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
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }
    

    private void Start()
    {
        dashSkill = instance.GetComponent<DashSkill>();
        cloneSkill = instance.GetComponent<CloneSkill>();
        swordSkill = instance.GetComponent<SwordSkill>();
        blackHoleSkill = instance.GetComponent<BlackHoleSkill>();
    }
}
