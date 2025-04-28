using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BossState
{
    public float idleTime = 3f;

    public IdleState(BossStateMachine boss, string animationName) : base(boss, animationName) { }

    public override void EnterState()
    {
        base.EnterState();
        boss.PlayFireCircleAnimation();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= boss.stateTime + idleTime)
        {
            boss.stateTime = Time.time;

            if (boss.ChecksForPlayerInChaseAttack())
            {
                boss.SwitchState(boss.chaseState);
                boss.projectileSpawn.SetActive(false);
            }


            if (boss.doStationaryAttack)
            {
                boss.projectileSpawn.SetActive(true);
                boss.doStationaryAttack = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

