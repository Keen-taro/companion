using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BossState
{
    public bool flip = true;

    public ChaseState(BossStateMachine boss, string animationName) : base(boss, animationName) { }

    public override void EnterState()
    {
        Debug.Log("Chasing Player");
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (boss.ChecksForPlayerInRangeAttack())
        {
            boss.rb2d.velocity = Vector2.zero;

            boss.SwitchState(boss.moveAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        ChasePlayer();

    }

    public void ChasePlayer()
    {
        Vector3 scale = boss.transform.localScale;
        Vector3 direction = boss.player.transform.position - boss.midCharacter.transform.position;

        // Determine the direction to move along the X axis
        if (direction.x > 0)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }

        // Constrain Y movement to a range between -1 and 1
        float yDirection = Mathf.Clamp(direction.y, -1, 1);

        // Translate the boss in both X and Y directions
        boss.transform.Translate(boss.stats.chaseSpeed * Time.deltaTime * Mathf.Sign(direction.x), boss.stats.chaseSpeed * Time.deltaTime * yDirection, 0);

        boss.transform.localScale = scale;
    }
}

