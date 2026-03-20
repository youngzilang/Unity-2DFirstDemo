using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackHoleState : PlayerState
{
    private float flyTime=.4f;
    private bool isSkill;
    private float originalG;
    public PlayerBlackHoleState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        originalG = player.rb.gravityScale;
        isSkill = false;
        stateTimer = flyTime;
        player.rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = originalG;
        player.Transprent(false);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer > 0)
        {
            player.rb.velocity = new Vector2(0, 15);
        }
        if (stateTimer < 0)
        {
            player.rb.velocity = new Vector2(0, -.1f);
            
        }
        if (!isSkill)
        {
           if( player.skillManager.blackHoleSkill.CanSkill())
            {
                isSkill = true;
                player.skillManager.blackHoleSkill.UseSkill();
            }
            
        }

        if (player.skillManager.blackHoleSkill.BlackHoleFinish())
        {
            player.stateMachine.ChangeState(player.fallState);
        }

    }
}
