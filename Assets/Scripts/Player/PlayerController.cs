using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton;

    [SerializeField] private Animator respawnTransition;

    //Spawn Point
    public Transform spawnPoint;

    //Audio Source
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioStepClip;
    [SerializeField] private AudioClip hurtAudioClip;
    [SerializeField] private AudioClip collectedMemory;

    //Footstep
    [SerializeField] private float footstepInterval = 0.5f;  // Time interval between footsteps
    [SerializeField] private float footstepRunInterval = 0.1f;  // Time interval between footsteps

    private float footstepTimer = 0f;

    // Movement
    public float movementSpeed, jumpForce;
    public bool isFacingRight, isJumping = false, isRunning, canMove;
    Rigidbody2D rb;

    // GroundChecker
    public float radius;
    public Transform groundChecker;
    public LayerMask whatIsGround;

    // Animation
    public Animator anim;
    string walk_animation = "Walk";
    string idle_animation = "Idle";
    string run_animation = "Run";

    float gridSize = 1.0f;

    private bool isDie;

    private void Awake()
    {
        singleton = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //canMove = false;
        transform.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
    }

    private void Start()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.x = Mathf.Round(spawnPosition.x / gridSize) * gridSize;
        spawnPosition.y = Mathf.Round(spawnPosition.y / gridSize) * gridSize;
        transform.position = spawnPosition;
    }

    private void Update()
    {
        Jump();
        //DeterminePushOrPull();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (isDie) return;
        if (!PlayerAttack.singleton.canAct) return;
        if (!canMove) return;

        //if (BoulderInteraction.singleton.isInteracting) return;  

        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * movementSpeed, rb.velocity.y);

        // Check if the player is moving
        if (move != 0)
        {
            footstepTimer += Time.deltaTime;

            if (isGrounded())
            {
                if (footstepTimer >= footstepInterval)
                {
                    PlayFootstepSound();
                    footstepTimer = 0f;
                }
            }

            // Running logic
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (footstepTimer >= footstepRunInterval && isGrounded())
                {
                    PlayFootstepSound();
                    footstepTimer = 0f;
                }

                isRunning = true;
                float run = movementSpeed + 4; // Adjust as needed for running speed
                rb.velocity = new Vector2(move * run, rb.velocity.y);
                anim.SetTrigger(run_animation); // Play running animation
            }
            else
            {
                isRunning = false;
                rb.velocity = new Vector2(move * movementSpeed, rb.velocity.y);
                anim.SetTrigger(walk_animation); // Play walking animation
            }
        }
        else
        {
            isRunning = false;
            anim.SetTrigger(idle_animation); // Play idle animation
        }

        // Flip character to face the correct direction
        if (move > 0 && !isFacingRight)
        {
            transform.eulerAngles = Vector2.zero;
            isFacingRight = true;
        }
        else if (move < 0 && isFacingRight)
        {
            transform.eulerAngles = Vector2.up * 180;
            isFacingRight = false;
        }
    }

    void Jump()
    {
        //if (playerAttack != null && !playerAttack.canAct) return;

        if (isDie) return;

        if (!canMove) return;

        // Initial jump logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            PlayFootstepSound();

            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("Jump");
            isJumping = true;
            /*
            anim.SetBool("isAirborne", true);  // Set airborne on jump
            anim.SetBool("isFalling", false);
            anim.SetBool("isLanding", false);
            */
        }

        /*
        // Check if character is airborne (upward movement)
        if (rb.velocity.y > 0 && !isGrounded())
        {
            anim.SetBool("isAirborne", true);
            anim.SetBool("isFalling", false);
            anim.SetBool("isLanding", false);
        }

        // Check if character is falling (downward movement)
        else if (rb.velocity.y < 0 && !isGrounded())
        {
            anim.SetBool("isAirborne", false);
            anim.SetBool("isFalling", true);
            anim.SetBool("isLanding", false);
        }

        // Check if character lands
        else if (isGrounded() && (anim.GetBool("isFalling") || isJumping))
        {
            anim.SetBool("isLanding", true); // Only trigger landing if falling or was jumping
            anim.SetBool("isAirborne", false);
            anim.SetBool("isFalling", false);
            isJumping = false; // Reset jump
        }
        */
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, radius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundChecker.position, radius);
    }

    private void PlayFootstepSound()
    {
        if(audioStepClip != null)
        {
            AudioClip footstepClip = audioStepClip[Random.Range(0, audioStepClip.Length)];
            audioSource.PlayOneShot(footstepClip);
        }
    }

    public void Die()
    {
        StartCoroutine(DieAndRespawn());
    }

    private IEnumerator DieAndRespawn()
    {
        // Wait for 2 seconds before marking the player as dead
        PlayerSanity.singleton.SanityDecreases(20f);
        Debug.Log("The player has died");
        isDie = true;

        if (isDie)
        {
            // Wait for another 5 seconds before respawning
            anim.SetBool("Hurt", true);

            if(hurtAudioClip != null)
            {
                audioSource.PlayOneShot(hurtAudioClip);
            }

            respawnTransition.SetTrigger("End");

            yield return new WaitForSeconds(2);
            Respawn();

            yield return new WaitForSeconds(1);

            respawnTransition.SetTrigger("Idle");
        }
    }

    private void Respawn()
    {
        transform.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
        anim.SetBool("Hurt", false);
        respawnTransition.SetTrigger("Start");
        isDie = false;
    }

    public void SetSpawnOnCheckPoint(Transform checkPoint)
    {
        spawnPoint.position = new Vector2(checkPoint.position.x, checkPoint.position.y);
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public void PlayCollectedSound()
    {
        audioSource.PlayOneShot(collectedMemory);
    }

    /*
    public void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.F) && BoulderInteraction.singleton.IsPlayerInRange())
        {
            BoulderInteraction.singleton.isInteracting = !BoulderInteraction.singleton.isInteracting; // Toggle interaction
            rb.velocity = Vector2.zero; // Stop boulder when interaction toggles

            if (BoulderInteraction.singleton.isInteracting)
            {
                DeterminePushOrPull(); // Determine if the player is pushing or pulling
            }
            else
            {
                anim.SetTrigger("Idle"); // Reset to idle animation when interaction ends
            }
        }
    }

    private void DeterminePushOrPull()
    {
        bool isPlayerOnLeft = BoulderInteraction.singleton.isPlayerOnLeft;

        // Check movement direction to determine pushing or pulling
        if (isPlayerOnLeft && Input.GetAxisRaw("Horizontal") > 0)
        {
            //Debug.Log("Player on the left");
            BoulderInteraction.singleton.isPushing = true; // Player on left, moving right -> pushing
        }
        else if (isPlayerOnLeft && Input.GetAxisRaw("Horizontal") < 0)
        {
            BoulderInteraction.singleton.isPushing = false; // Player on left, moving left -> pulling
            //anim.SetTrigger("Pull");
        }
        else if (!isPlayerOnLeft && Input.GetAxisRaw("Horizontal") < 0)
        {
            //Debug.Log("Player on the Right");
            BoulderInteraction.singleton.isPushing = true; // Player on right, moving left -> pushing
            //anim.SetTrigger("Push");
        }
        else if (!isPlayerOnLeft && Input.GetAxisRaw("Horizontal") > 0)
        {
            BoulderInteraction.singleton.isPushing = false; // Player on right, moving right -> pulling
            //anim.SetTrigger("Pull");
        }
    }
    */
}