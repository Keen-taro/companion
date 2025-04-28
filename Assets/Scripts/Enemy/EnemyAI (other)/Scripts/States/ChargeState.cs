using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : EnemyBaseState
{
    public ChargeState(EnemyMobs enemy, string animationName) : base(enemy, animationName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.footstepTimer >= enemy.footstepRunInterval)
        {
            enemy.PlayFootstepSound();
            enemy.footstepTimer = 0f;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if(Time.time >= enemy.stateTime + enemy.stats.chargeTime)
        {
            if (enemy.ChecksForPlayer())
            {
                enemy.SwitchState(enemy.playerDetectState);
            }
            else
            {
                enemy.SwitchState(enemy.patrolState);
            }
        }
        else
        {
            if (enemy.CheckForMeleeTarget())
            {
                enemy.SwitchState(enemy.meleeAttackState);
            }
            Charge();
        }
    }

    void Charge()
    {
        enemy.rb.velocity = new Vector2(enemy.stats.chargeSpeed * enemy.facingDirection, enemy.rb.velocity.y);
    }
}
