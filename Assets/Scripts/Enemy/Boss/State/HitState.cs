using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : BossState
{
    public float KBForce;
    public Vector2 KBAngle;

    public float recoverTime = 1;

    public HitState(BossStateMachine boss, string animationName) : base(boss, animationName) { }

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
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

        if (Time.time > boss.stateTime + recoverTime)
        {
            boss.stateTime = Time.time;

            boss.SwitchState(boss.vanishState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

