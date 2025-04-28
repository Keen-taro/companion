using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    protected BossStateMachine boss;
    protected string animationName;

    public BossState(BossStateMachine boss, string animationName)
    {
        this.boss = boss;
        this.animationName = animationName;
    }

    public virtual void EnterState()
    {
        boss.animator.SetBool(animationName, true);
    }

    public virtual void ExitState()
    {
        boss.animator.SetBool(animationName, false);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void AnimationFinishedTrigger() { }

    public virtual void AnimationAttackTrigger() { }
}

