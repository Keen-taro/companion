using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderInteraction : MonoBehaviour
{
    public static BoulderInteraction singleton;

    public float pullSpeed, pushSpeed; // Speed of boulder movement

    private GameObject playerObject;
    private Transform player;    // Reference to the player
    public Animator playerAnimator; // Animator for the player

    public Collider2D interactCollider; // Interaction trigger collider for the boulder
    public Collider2D boulderCollider; // Physical collider of the boulder
    private Rigidbody2D rb; // Rigidbody of the boulder
    

    public bool isInteracting = false; // Is the player interacting with the boulder?
    public bool isPushing = false; // Determines if pushing or pulling

    public bool isPlayerOnLeft;

    public Transform Left, Right;

    private void Awake()
    {
        singleton = this;
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;
        playerAnimator = playerObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //PlayerController.singleton.HandleInteraction(); // Check for interaction input
        //HandleBoulderMovement(); // Handle movement of the boulder
        //CheckPlayerPosition();
    }

    public bool IsPlayerInRange()
    {
        // Check if the player's collider overlaps with the interaction trigger
        return interactCollider.bounds.Intersects(player.GetComponent<Collider2D>().bounds);
    }

    public void CheckPlayerPosition()
    {
        isPlayerOnLeft = player.position.x < transform.position.x;
    }

    void HandleBoulderMovement()
    {
        if (!isInteracting) return; // Do nothing if not interacting

        float move = Input.GetAxisRaw("Horizontal"); // Get horizontal input

        if (move != 0)
        {

            if (isPlayerOnLeft) //Leftside
            {
                if (!PlayerController.singleton.isFacingRight)
                {
                    PlayerController.singleton.transform.eulerAngles = Vector2.zero;
                }

                player.transform.position = new Vector2(Left.position.x, player.transform.position.y);
                if (move > 0) // Player moving right
                {
                    isPushing = true; // Pushing
                    rb.position += new Vector2(move * pushSpeed, rb.velocity.y); // Move boulder to the right
                    PlayerController.singleton.anim.SetTrigger("Push");
                }
                else if (move < 0) // Player moving left
                {
                    isPushing = false; // Pulling
                    rb.position += new Vector2(move * pullSpeed, rb.velocity.y); // Move boulder to the right                                                                                   // Move boulder to the left
                    PlayerController.singleton.anim.SetTrigger("Pull");
                }
            }
            else if (!isPlayerOnLeft) //Rightside
            {
                if (PlayerController.singleton.isFacingRight)
                {
                    PlayerController.singleton.transform.eulerAngles = Vector2.up * 180;
                }

                player.transform.position = new Vector2(Right.position.x, player.transform.position.y);

                if (move < 0) // Player moving left
                {
                    isPushing = true; // Pushing
                    rb.position += new Vector2(move * pushSpeed, rb.velocity.y); // Move boulder to the left
                    PlayerController.singleton.anim.SetTrigger("Push");
                }
                else if (move > 0) // Player moving right
                {
                    isPushing = false; // Pulling
                    rb.position += new Vector2(move * pullSpeed, rb.velocity.y); // Move boulder to the right
                    PlayerController.singleton.anim.SetTrigger("Pull");
                }
            }
        }
        else
        {
            // Stop boulder when no input is detected
            rb.velocity = Vector2.zero;
        }


    }
}
