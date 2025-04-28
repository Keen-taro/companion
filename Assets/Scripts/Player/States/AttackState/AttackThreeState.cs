using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackThreeState : Player
{
    public AttackThreeState(PlayerStateMachine player, string animationName) : base(player, animationName) { }

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();

        player.attackCollider.SetActive(true);
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
        player.attackCollider.SetActive(false);
        player.canMove = true;

        if (player.playerStats.move == 0)
        {
            player.SwitchState(player.idleState);
        }
        else if (player.playerStats.move != 0)
        {
            player.SwitchState(player.moveState);
        }

        player.playerStats.attackAvailableTime = Time.time + 2f;
    }

    public override void EnterState()
    {
        base.EnterState();
        player.attackColliderHandler.SetAttackProperties(player.playerStats.attackDamage[2], player.playerStats.knockbackAngle[2], player.playerStats.knockbackForce[2]);
        player.canMove = false;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
