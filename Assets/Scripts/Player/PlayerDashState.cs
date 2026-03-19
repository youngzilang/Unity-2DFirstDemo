using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float dashFace;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        SkillManager.instance.cloneSkill.ClonePrefab(player.transform,0);

        dashContinueTimer = player.dashContinue;
        
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVe(0, 0);
    }

    public override void Update()
    {
        base.Update();

        if (inputX != 0) dashFace = inputX;
        else dashFace = player.faceDir;

        if (player.WallCheck() && !player.GroundCheck()) stateMachine.ChangeState(player.wallSlideState);


        player.SetVe(player.dashSpeed*dashFace, 0);

        if (dashContinueTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
