using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public DashSkill dashSkill { get; private set; }
    public CloneSkill cloneSkill { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else instance = this;
    }
    

    private void Start()
    {
        dashSkill = instance.GetComponent<DashSkill>();
        cloneSkill = instance.GetComponent<CloneSkill>();
    }
}
