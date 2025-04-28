using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMobs : MonoBehaviour, IDamageable
{
    #region Variables
    public EnemyBaseState currectState;

    public PatrolState patrolState;
    public PlayerDetectedState playerDetectState;
    public ChargeState chargeState;
    public MeleeAttackState meleeAttackState;
    public RangeAttackstate rangeAttackState;

    public DamageState damageState;
    public DeathStates deathStates;

    public float footstepInterval = 0.5f;  // Time interval between footsteps
    public float footstepRunInterval = 0.1f;  // Time interval between footsteps
    public float footstepTimer = 0f;

    public Animator anim;
    public Rigidbody2D rb;
    public Transform ledgeDetector;
    public LayerMask groundLayer, playerLayer, damageableLayer;

    public int facingDirection = 1;
    public float stateTime;

    public StatsSO stats;
    public float currenHealth;

    public AudioClip hitAudio;
    public AudioClip[] walk;

    public AudioSource audioSource;

    [Header("Item Drop Variables")]
    public GameObject[] itemDrops;
    public float dropForce;
    public float torque; 

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        patrolState = new PatrolState(this, "patrol");
        playerDetectState = new PlayerDetectedState(this, "playerDetect");
        chargeState = new ChargeState(this, "charge");
        meleeAttackState = new MeleeAttackState(this, "meleeAttack");
        //rangeAttackState = new RangeAttackstate(this, "rangeAttack");
        damageState = new DamageState(this, "hit");
        deathStates = new DeathStates(this, "die");

        currectState = patrolState;
        currectState.Enter();
    }

    private void Start()
    {
        currenHealth = stats.maxHealth;
    }

    private void Update()
    {
        currectState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currectState.PhysicsUpdate();
    }
    #endregion

    #region Checks
    public bool ChecksForObstacle()
    {
        RaycastHit2D hitCliff = Physics2D.Raycast(ledgeDetector.position, Vector2.down, stats.cliffDistance, groundLayer);

        if (hitCliff.collider == null || hitCliff.collider.CompareTag("Nulify"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ChecksForPlayer()
    {
        RaycastHit2D hitPlayer = Physics2D.Raycast(ledgeDetector.position, facingDirection == 1 ? Vector2.right : Vector2.left, stats.playerDetectDistance, playerLayer);

        if (hitPlayer.collider)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public bool CheckForMeleeTarget()
    {
        RaycastHit2D hitMelee = Physics2D.Raycast(ledgeDetector.position, facingDirection == 1 ? Vector2.right : Vector2.left, stats.meleeDistance, playerLayer);

        if (hitMelee.collider)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*
    public bool CheckForRangeTarget()
    {
        RaycastHit2D hitRange = Physics2D.Raycast(transform.position, facingDirection == 1 ? Vector2.right : Vector2.left, stats.rangeDistance, playerLayer);

        if (hitRange.collider)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    */

    #endregion

    #region Other


    public void Rotate()
    {
        transform.Rotate(0, 180, 0);
        facingDirection = -facingDirection;
    }

    public void SwitchState(EnemyBaseState newState)
    {
        currectState.Exit();
        currectState = newState;
        currectState.Enter();
        stateTime = Time.time;
    }

    public void AnimationFinishedTrigger()
    {
        currectState.AnimationFinishedTrigger();
    }

    public void AnimationAttackTrigger()
    {
        currectState.AnimationAttackTrigger();
    }

    private void OnDrawGizmos()
    {
        // Set the color for the Gizmo
        Gizmos.color = Color.green;

        // Define the starting position of the ray (ledgeDetector's position)
        Vector2 startPosition = ledgeDetector.position;

        // Define the direction (Vector2.down) and the length of the ray
        Vector2 direction = Vector2.down;
        float length = stats.cliffDistance; // Use your cliff distance value

        // Draw the ray as a line
        Gizmos.DrawLine(startPosition, startPosition + direction * length);

        Gizmos.DrawWireSphere(ledgeDetector.position, stats.meleeDistance);
    }

    public void Damage(float damageAmount) { } 

    public void Damage(float damageAmount, float KBForce, Vector2 KBAngle)
    {
        audioSource.PlayOneShot(hitAudio);
        damageState.KBForce = KBForce;
        damageState.KBAngle = KBAngle;
        currenHealth -= damageAmount;

        if(currenHealth <= 0)
        {
            SwitchState(deathStates);
        }
        else
        {
            SwitchState(damageState);
        }
    }

    public void PlayFootstepSound()
    {
        if (walk != null)
        {
            AudioClip footstep = walk[Random.Range(0, walk.Length)];
            audioSource.PlayOneShot(footstep);
        }
    }

    public void Instantiate(GameObject prefab, float force, float torque)
    {
        Rigidbody2D itemRB = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        Vector2 dropVelocity = new Vector2(Random.Range(.5f, -.5f), 1) * dropForce;
        itemRB.AddForce(dropVelocity, ForceMode2D.Impulse);
        itemRB.AddTorque(torque, ForceMode2D.Impulse);
    }

    #endregion

}
