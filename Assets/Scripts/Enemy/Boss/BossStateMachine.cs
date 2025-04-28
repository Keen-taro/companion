using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour, IDamageable
{
    public Transform player;

    public LayerMask damageableLayer;
    public LayerMask playerLayerMask;
    public Transform middle, midCharacter, attackPosition;
    public Transform leftHit, rightHit;
    public BossStats stats; // Contains health, range, and other stats
    public Animator animator;

    public float stateTime;
    public float health;
    public double damageTaken;

    public bool doStationaryAttack;
    public bool doAirAttack;
    public int vanishCount;

    public Animator circleAnimator;

    private BossState currentState;
    public Rigidbody2D rb2d;

    public int facingDirection = 1;

    public GameObject projectileSpawn;

    #region State

    public AppearState appearState;
    public IdleState idleState;
    public VanishState vanishState;
    public ChaseState chaseState;
    public MoveAttackPrepState moveAttackPrepState;
    public MoveAttackState moveAttackState;
    public StopAfterAttackState stopAfterAttackState;
    public StationaryAttackState stationaryAttack;
    public AirAttackState airAttackState;
    public HitState hitState;
    public DieState deathState;

    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        health = stats.maxHealthPoint;

        appearState = new AppearState(this, "Appear");
        idleState = new IdleState(this, "IdleLow");
        vanishState = new VanishState(this, "Vanish");
        chaseState = new ChaseState(this, "StartMoving");
        moveAttackPrepState = new MoveAttackPrepState(this, "MoveAttackPrep");
        moveAttackState = new MoveAttackState(this, "MoveAttack");
        stopAfterAttackState = new StopAfterAttackState(this, "StopMoving");
        stationaryAttack = new StationaryAttackState(this, "StationaryAttack");
        airAttackState = new AirAttackState(this, "AirAttack");
        hitState = new HitState(this, "Hit");
        deathState = new DieState(this, "Death");

        currentState = appearState;
        currentState.EnterState();
    }

    private void Update()
    {
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }

    public void SwitchState(BossState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
        stateTime = Time.time;
    }

    public bool IsDead()
    {
        return (health <= 0);
    }

    public void HandleDeath()
    {
        gameObject.SetActive(false);
    }

    public bool ChecksForPlayerInRangeAttack()
    {
        return Physics2D.OverlapCircle(midCharacter.transform.position, stats.attackRange, playerLayerMask);
    }

    public bool ChecksForPlayerInChaseAttack()
    {
        return Physics2D.OverlapCircle(midCharacter.transform.position, stats.chaseRange, playerLayerMask);
    }

    public bool TenPercentDamageTaken()
    {
        return damageTaken % (stats.maxHealthPoint * 0.1) == 0 || vanishCount == 4;
    }

    public void RecoverFromHit()
    {
        // Implement recovery logic after being hit
    }

    public void PlayFireCircleAnimation()
    {
        circleAnimator.Play("fireCircle");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(midCharacter.transform.position, stats.attackRange); // Attack range

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(midCharacter.transform.position, stats.chaseRange); // Chase range

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(middle.transform.position, stats.teleportRange); // Teleport range
    }

    public void AnimationFinishedTrigger()
    {
        currentState.AnimationFinishedTrigger();
    }

    public void AnimationAttackTrigger()
    {
        currentState.AnimationAttackTrigger();
    }

    public void Damage(float damageAmount) { }

    public void Damage(float damageAmount, float KBForce, Vector2 KBAngle)
    {
        health -= damageAmount;
        hitState.KBForce = 0;
        hitState.KBAngle = new Vector2(0,0);
        damageTaken += damageAmount;

        if (health <= 0)
        {
            SwitchState(deathState);
        }
        else
        {
            SwitchState(hitState);
        }
    }
}

