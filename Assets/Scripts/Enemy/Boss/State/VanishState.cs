using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishState : BossState
{
    public float vanishTime = 1f;

    public VanishState(BossStateMachine boss, string animationName) : base(boss, animationName) { }

    public override void EnterState()
    {
        base.EnterState();
        boss.vanishCount++;
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

        if (Time.time >= boss.stateTime + vanishTime)
        {
            boss.stateTime = Time.time;
            boss.transform.position = boss.middle.position;
            boss.doStationaryAttack = true;

            boss.SwitchState(boss.appearState);
        }
    }

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}

