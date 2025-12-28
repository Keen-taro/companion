using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControllerSimplified : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 1f;
    public float runSpeed = 3f;
    public float jumpForce = 10f;

    private float currentSpeed;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded = false;
    private bool isJumping = false;
    public bool isInCutScene = false;
    public bool canMove = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    public Transform PlayerSpawnPoint;

    public GameObject Button;

void Start()
    {
        //canMove = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        transform.position = PlayerSpawnPoint.position;
    }

    void Update()
    {
        CheckGround();

        Move();
        Jump();
        UpdateAnimator();

        //Debug.Log(Input.GetAxis("Horizontal"));
    }


    void Move()
    {
        // Jika sedang lompat, kunci arah
        if (isJumping) return;
        if (isInCutScene) return;

        if (!canMove) 
        {
            rb.velocity = Vector2.zero;
            return;
        }
            
        float x = Input.GetAxisRaw("Horizontal");
        bool running = Input.GetKey(KeyCode.LeftShift);

        currentSpeed = running ? runSpeed : walkSpeed;

        rb.velocity = new Vector2(x * currentSpeed, rb.velocity.y);

        // Flip arah sprite
        if (x != 0)
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
    }


    void Jump()
    {
        if (isInCutScene) return;
        if (!canMove) return;

        // tombol loncat ditekan + sedang di tanah
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("Jump");

            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            isJumping = true; // Lock movement saat di udara
        }
    }


    void CheckGround()
    {
        bool wasGrounded = isGrounded;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        //Debug.Log(isGrounded);

        // Jika baru saja mendarat
        if (!wasGrounded && isGrounded)
        {
            isJumping = false;  // Unlock movement
        }
    }

    void UpdateAnimator()
    {

        float speedMagnitude = Mathf.Abs(rb.velocity.x);

        animator.SetFloat("Speed", speedMagnitude);
        animator.SetBool("IsGrounded", isGrounded);
    }

    public void ChangeStatusCanMove()
    {
        canMove = !canMove;
    }

    public void ChangeStatusIsInCutScene()
    {
        isInCutScene = !isInCutScene;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cawan"))
        {
            Button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cawan"))
        {
            Button.SetActive(false);
        }
    }
}
