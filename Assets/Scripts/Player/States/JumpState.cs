using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : Player
{
    public JumpState(PlayerStateMachine player, string animationName) : base(player, animationName) { }

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void EnterState()
    {
        base.EnterState();
        player.playerStats.jumpCooldown = Time.time + 0.3f;

        player.rb.AddForce(Vector2.up * player.playerStats.jumpForce, ForceMode2D.Impulse);

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.isGrounded() && player.playerStats.move != 0)
        {
            player.SwitchState(player.moveState);
        }
        else if (player.isGrounded() && player.playerStats.move == 0)
        {
            player.SwitchState(player.idleState);
        }

        if (Input.GetMouseButtonDown(1) && player.playerStats._canDash && !player.isGrounded())
        {
            player.SwitchState(player.jumpDashState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
