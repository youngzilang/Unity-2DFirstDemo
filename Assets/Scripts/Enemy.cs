using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public Rigidbody2D rigidbody { get; private set; }
    public Animator animator { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
