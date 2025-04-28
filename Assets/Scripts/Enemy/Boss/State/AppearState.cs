using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearState : BossState
{
    public float idleTime = 1f;

    public AppearState(BossStateMachine boss, string animationName) : base(boss, animationName) { }

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

        if (Time.time >= boss.stateTime + idleTime)
        {
            boss.stateTime = Time.time;

            boss.SwitchState(boss.idleState);
        }
    }
}
