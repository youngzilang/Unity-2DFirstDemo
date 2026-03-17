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
        player.StartCoroutine("Busy", .2f);
    }

    public override void Update()
    {
        base.Update();
        player.SetVe(0, 0);

        if (Input.GetKeyUp(KeyCode.Mouse1)) stateMachine.ChangeState(player.idleState);

        Vector2 aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (aimDir.x > player.transform.position.x && player.faceDir == -1) player.Flip();
        else if (aimDir.x < player.transform.position.x && player.faceDir == 1) player.Flip();
    }
}