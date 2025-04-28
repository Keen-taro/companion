using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerStateMachine : MonoBehaviour, IDamageable
{
    #region Player Component
    public Animator animator;

    public Transform spawnPoint;
    public Transform groundChecker;

    public TrailRenderer _trailRenderer;

    public bool isFacingRight;
    public bool isJumping = false;
    public bool isRunning;
    public bool isReading = false;
    public bool canMove;

    public float sanity;
    public float stateTime;

    public PlayerStats playerStats;

    public LayerMask whatIsGround;

    private Player currentState;
    [SerializeField] public Animator respawnTransition;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioStepClip;
    [SerializeField] private AudioClip hurtAudioClip;
    [SerializeField] private AudioClip collectedMemory;

    public Rigidbody2D rb;

    //Attack Attribute
    public GameObject attackCollider;
    public AttackColliderHandler attackColliderHandler;

    #endregion

    #region State

    public IdleStatePlayer idleState;
    public MovingState moveState;
    public JumpState jumpState;
    public DashState dashState;
    public RunState runState;
    public DeathState deathState;
    public JumpDashState jumpDashState;

    public AttackOneState attackCombo1;
    public AttackTwoState attackCombo2;
    public AttackThreeState attackCombo3;

    #endregion


    private void Start()
    {
        transform.position = spawnPoint.position;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        transform.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);

        sanity = playerStats.maxSanityPoint;
        playerStats.dashingTime = 0;
        canMove = false;
        playerStats.isDie = false;
        playerStats._canDash = true;

        playerStats.attackAvailableTime = 0;
        playerStats.attackComboTimer = 0;
        playerStats.jumpCooldown = 0;

        idleState = new IdleStatePlayer(this, "Idle");
        moveState = new MovingState(this, "Walk");
        jumpState = new JumpState(this, "Jump");
        jumpDashState = new JumpDashState(this, "JumpDash");
        dashState = new DashState(this, "Dash");
        runState = new RunState(this, "Run");
        deathState = new DeathState(this, "Hurt");

        attackCombo1 = new AttackOneState(this, "AttackOne");
        attackCombo2 = new AttackTwoState(this, "AttackTwo");
        attackCombo3 = new AttackThreeState(this, "AttackThree");


        currentState = idleState;
        currentState.EnterState();
    }

    private void Update()
    {
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate();

        playerStats.move = Input.GetAxisRaw("Horizontal");

        if (playerStats.dashingTime >= 0)
        {
            playerStats.dashingTime -= Time.deltaTime;
            playerStats._canDash = false;
        }
        else
        {
            playerStats._canDash = true;
        }
    }

    public void SwitchState(Player newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
        stateTime = Time.time;
    }

    #region Ground Check

    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, playerStats.radius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundChecker.position, playerStats.radius);
    }

    #endregion

    #region Animation Start or Finish

    public void AnimationFinishedTrigger()
    {
        currentState.AnimationFinishedTrigger();
    }

    public void AnimationAttackTrigger()
    {
        currentState.AnimationAttackTrigger();
    }

    #endregion

    #region Status 

    public void SetSpawnOnCheckPoint(Transform checkPoint)
    {
        spawnPoint.position = new Vector2(checkPoint.position.x, checkPoint.position.y);
    }

    public void SanityIncreases(float addSanity)
    {
        sanity += addSanity;
    }

    public void SanityDecreases(float minusSanity)
    {
        sanity += minusSanity;
    }

    public void Damage(float damageAmount)
    {
        //audioSource.PlayOneShot(hitSound);
        Debug.Log("Player Hit");

        sanity -= damageAmount;
    }

    public void Damage(float damageAmount, float KBForce, Vector2 KBAngle)
    {
        Debug.Log("Player Hit");
        rb.velocity = KBAngle * KBForce;
        sanity -= damageAmount;
    }

    #endregion

    public void PlayCollectedSound()
    {
        //audioSource.PlayOneShot(collectedMemory);
        Debug.Log("Memory Collected");
    }
}
