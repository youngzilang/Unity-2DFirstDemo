using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cd;
    protected float cdTimer;

    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }

    protected virtual void Update()
    {
        cdTimer -= Time.deltaTime;
    }

    public virtual bool CanSkill()
    {
        if (cdTimer < 0)
        {
            cdTimer = cd;
            return true;
        }
        return false;
    }

    public virtual void UseSkill()
    {

    }
}
