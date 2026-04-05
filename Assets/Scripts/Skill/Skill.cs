using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] public float cd;
    protected float cdTimer;

    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
        CheckUnlock();
    }

    protected virtual void Update()
    {
        cdTimer -= Time.deltaTime;
    }

    protected virtual void CheckUnlock()
    {

    }

    public virtual bool CanSkill()
    {
        if (cdTimer < 0)
        {
            UseSkill();
            cdTimer = cd;
            return true;
        }
        return false;
    }

    public virtual void UseSkill()
    {

    }
}
