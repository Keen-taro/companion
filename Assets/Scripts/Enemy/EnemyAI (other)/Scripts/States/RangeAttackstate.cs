using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackstate : EnemyBaseState
{
    public RangeAttackstate(EnemyMobs enemy, string animationName) : base(enemy, animationName)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationAttackTrigger()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(enemy.transform.position, enemy.stats.rangeDistance, enemy.damageableLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                hitCollider.GetComponent<Rigidbody2D>().velocity = new Vector2(enemy.stats.knockbackAngle.x * enemy.facingDirection,
                    enemy.stats.knockbackAngle.y) * enemy.stats.knockbackForce;
                damageable.Damage(enemy.stats.damageAmount);
            }
        }

        base.AnimationAttackTrigger();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        enemy.SwitchState(enemy.patrolState);
    }
}