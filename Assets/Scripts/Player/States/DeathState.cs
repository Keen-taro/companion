using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : Player
{
    public DeathState(PlayerStateMachine player, string animationName) : base(player, animationName) { }

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
        
        player.StartCoroutine(Res());

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

    private void Respawn()
    {
        player.transform.position = new Vector2(player.spawnPoint.position.x, player.spawnPoint.position.y);
        player.respawnTransition.SetTrigger("Start");
    }

    private IEnumerator Res()
    {
        Die();
        /*
        if (hurtAudioClip != null) { audioSource.PlayOneShot(hurtAudioClip); }
        */

        player.respawnTransition.SetTrigger("End");

        yield return new WaitForSeconds(1.5f);

        Respawn();

        player.respawnTransition.SetTrigger("Idle");

        yield return new WaitForSeconds(0.5f);

        player.playerStats.isDie = false;
        player.SwitchState(player.idleState);

    }

    private void Die()
    {
        player.SanityDecreases(20f);
        Debug.Log("The player has died");
        player.playerStats.isDie = true;
    }
}
