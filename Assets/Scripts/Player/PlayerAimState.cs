using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAimState : PlayerState
{
    public PlayerAimState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skillManager.swordSkill.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse1)) stateMachine.ChangeState(player.idleState);
    }
}