using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOneState : Player
{
    public AttackOneState(PlayerStateMachine player, string animationName) : base(player, animationName) { }

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

    }

    public override void EnterState()
    {
        base.EnterState();
        player.playerStats.attackComboTimer = Time.time + 1f;
        player.playerStats.attackAvailableTime = Time.time + 0.35f;

        player.attackColliderHandler.SetAttackProperties(player.playerStats.attackDamage[1], player.playerStats.knockbackAngle[1], player.playerStats.knockbackForce[1]);
        player.canMove = false;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Input.GetMouseButtonDown(0) && Time.time >= player.playerStats.attackAvailableTime)
        {
            player.SwitchState(player.attackCombo2);
        }
        else if (Time.time >= player.playerStats.attackComboTimer)
        {
            Debug.Log("End of ComboTimer");

            player.SwitchState(player.idleState);
            player.animator.SetBool("AttackOne", false);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
