using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStatePlayer : Player
{
    public IdleStatePlayer(PlayerStateMachine player, string animationName) : base(player, animationName) { }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Player on Idle state");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.isReading) return; 

        if (player.playerStats.isDie) return;

        if (player.playerStats._isDashing) return;

        if (!player.canMove) return;

        if (player.playerStats.move != 0)
        {
            player.SwitchState(player.moveState);
        }

        if (Input.GetKeyDown(KeyCode.W) && player.isGrounded() && Time.time >= player.playerStats.jumpCooldown)
        {
            player.SwitchState(player.jumpState);
        }

        if (Input.GetMouseButtonDown(0) && player.isGrounded() && Time.time >= player.playerStats.attackAvailableTime) 
        {
            player.SwitchState(player.attackCombo1);
        }

        if (Input.GetMouseButtonDown(1) && player.playerStats._canDash && player.isGrounded())
        {
            player.SwitchState(player.dashState);
        }

        if (Input.GetKey(KeyCode.LeftShift) && player.isGrounded() && player.playerStats.move != 0)
        {
            player.SwitchState(player.runState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
