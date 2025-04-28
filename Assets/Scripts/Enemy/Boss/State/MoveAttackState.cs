using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttackState : BossState
{
    public MoveAttackState(BossStateMachine boss, string animationName) : base(boss, animationName) { }

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(boss.midCharacter.position, boss.stats.attackRange, boss.damageableLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            IDamageable damageable = hitCollider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                hitCollider.GetComponent<Rigidbody2D>().velocity = new Vector2(boss.stats.knockbackAngleMelee.x * boss.facingDirection,
                    boss.stats.knockbackAngleMelee.y) * boss.stats.knockbackForceMelee;
                damageable.Damage(boss.stats.damageMeleeAttack);
            }
        }
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        boss.SwitchState(boss.idleState);
    }

    public override void EnterState()
    {
        base.EnterState();
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

