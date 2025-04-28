using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player
{
    protected PlayerStateMachine player;
    protected string animationName;

    public Player(PlayerStateMachine player, string animationName)
    {
        this.player = player;
        this.animationName = animationName;
    }

    public virtual void EnterState()
    {
        player.animator.SetBool(animationName, true);
    }

    public virtual void ExitState()
    {
        player.animator.SetBool(animationName, false);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void AnimationFinishedTrigger() { }

    public virtual void AnimationAttackTrigger() { }
}
