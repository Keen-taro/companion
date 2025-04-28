using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : Player
{
    public MovingState(PlayerStateMachine player, string animationName) : base(player, animationName) { }

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

        Debug.Log("Player Moving (Walk)");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.playerStats.isDie) return;

        if (player.playerStats._isDashing) return;

        if (player.playerStats.move == 0 && player.isGrounded())
        {
            player.SwitchState(player.idleState);
        }

        if (Input.GetKeyDown(KeyCode.W) && player.isGrounded() && Time.time >= player.playerStats.jumpCooldown)
        {
            player.SwitchState(player.jumpState);
        }

        if (Input.GetMouseButtonDown(1) && player.playerStats._canDash && player.isGrounded())
        {
            player.SwitchState(player.dashState);
        }

        if (Input.GetKey(KeyCode.LeftShift) && player.isGrounded())
        {
            player.SwitchState(player.runState);
        }

        if (Input.GetMouseButtonDown(0) && player.isGrounded() && Time.time >= player.playerStats.attackAvailableTime)
        {
            player.SwitchState(player.attackCombo1);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.rb.velocity = new Vector2(player.playerStats.move * player.playerStats.moveSpeed, player.rb.velocity.y);

        if (player.playerStats.move > 0 && !player.isFacingRight)
        {
            player.transform.eulerAngles = Vector2.zero;
            player.isFacingRight = true;
        }
        else if (player.playerStats.move < 0 && player.isFacingRight)
        {
            player.transform.eulerAngles = Vector2.up * 180;
            player.isFacingRight = false;
        }
    }
}
