using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;
    protected string aniName;


    protected float stateTimer;
    protected bool trigger;

    public EnemyState(Enemy enemy,EnemyStateMachine stateMachine,string aniName)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.aniName = aniName;
    }

    public virtual void Enter()
    {
        trigger = false;
        enemy.animator.SetBool(aniName, true);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public  virtual void Exit()
    {
        enemy.animator.SetBool(aniName, false);
    }
}
