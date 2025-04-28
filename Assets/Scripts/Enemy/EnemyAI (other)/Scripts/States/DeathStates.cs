using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStates : EnemyBaseState
{
    public DeathStates(EnemyMobs enemy, string animationName) : base(enemy, animationName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        DeathParticle();
        DropItem();
        enemy.gameObject.SetActive(false);
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
        base.AnimationAttackTrigger();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    private void DeathParticle()
    {
        if (enemy.stats.deathDebris != null)
        {
            foreach (var debris in enemy.stats.deathDebris)
            {
                enemy.Instantiate(debris, enemy.dropForce, enemy.torque);
            }
        }
    }

    private void DropItem()
    {
        if (enemy.itemDrops != null)
        {
            foreach (var item in enemy.itemDrops)
            {
                enemy.Instantiate(item, enemy.dropForce, enemy.torque);
            }
        }
    }
}
