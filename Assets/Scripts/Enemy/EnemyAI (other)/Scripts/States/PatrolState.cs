using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public PatrolState(EnemyMobs enemy, string animationName) : base (enemy, animationName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.footstepTimer >= enemy.footstepInterval)
        {
            enemy.PlayFootstepSound();
            enemy.footstepTimer = 0f;
        }

        if (enemy.ChecksForPlayer())
        {
            enemy.SwitchState(enemy.playerDetectState);
        }

        if (enemy.ChecksForObstacle())
        {
            enemy.Rotate();
        }
        
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (enemy.facingDirection == 1)
        {
            enemy.rb.velocity = new Vector2(enemy.stats.speed, enemy.rb.velocity.y);
        }
        else
        {
            enemy.rb.velocity = new Vector2(-enemy.stats.speed, enemy.rb.velocity.y);
        }
    }
}
