using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerState
{

    protected float inputX;
    protected float inputY;
    protected float dashContinueTimer;
    

    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected string animationName;

    public PlayerState(Player player,PlayerStateMachine stateMachine,string animationName)
    {
        this.player = player;
        this.stateMachine = stateMachine
;        this.animationName = animationName;
    }

    

    public virtual void Enter()
    {
        player.animator.SetBool(animationName, true);
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animationName, false);
    }

    public virtual void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        player.animator.SetFloat("yV", player.rb.velocity.y);
        dashContinueTimer -= Time.deltaTime;
    }
}
