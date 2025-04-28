using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : Player
{
    public DashState(PlayerStateMachine player, string animationName) : base(player, animationName) { }

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        if (player.isGrounded() && player.playerStats.move != 0)
        {
            player.SwitchState(player.moveState);
        }

        if (player.isGrounded() && player.playerStats.move == 0)
        {
            player.SwitchState(player.idleState);
        }
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Player Dashing");

        player.playerStats.dashingTime = 3f;

        if (player.isFacingRight)
        {
            player.StartCoroutine(Dash(1f));
        }
        else if (!player.isFacingRight)
        {
            player.StartCoroutine(Dash(-1f));
        }

        player._trailRenderer.emitting = true;
        player.playerStats._isDashing = true;
        player.playerStats._canDash = false;
    }

    public override void ExitState()
    {
        base.ExitState();

        player._trailRenderer.emitting = false;
        player.playerStats._isDashing = false;
        player.playerStats._canDash = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    IEnumerator Dash(float direction)
    {
        player.playerStats._isDashing = true;
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0f);
        player.rb.AddForce(new Vector2(player.playerStats.dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = player.rb.gravityScale;
        player.rb.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        player.playerStats._isDashing = false;
        player.rb.gravityScale = gravity;
    }
}
