using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of movement
    public bool moveHorizontal = true;  // If true, move horizontally (X-axis)
    public bool moveVertical = false;  // If true, move vertically (Y-axis)

    public float maxDistance = 5f;  // Maximum distance the object can move
    private float startPosition;  // Initial position when the movement starts
    private bool movingForward = true;  // Direction of movement

    private Transform playerTransform;  // To store the player's transform

    void Start()
    {
        // Save the initial position based on horizontal or vertical direction
        startPosition = moveHorizontal ? transform.position.x : transform.position.y;
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;
        float currentPosition = moveHorizontal ? transform.position.x : transform.position.y;

        // Move the platform in the specified direction
        if (moveHorizontal)
        {
            if (movingForward)
                movement.x = moveSpeed * Time.deltaTime;
            else
                movement.x = -moveSpeed * Time.deltaTime;
        }

        if (moveVertical)
        {
            if (movingForward)
                movement.y = moveSpeed * Time.deltaTime;
            else
                movement.y = -moveSpeed * Time.deltaTime;
        }

        // Apply platform movement
        transform.Translate(movement);

        // Move the player along with the platform if the player is on it
        if (playerTransform != null)
        {
            playerTransform.position += movement;
        }

        // Check if the platform has reached the max distance
        if (moveHorizontal)
        {
            if (Mathf.Abs(transform.position.x - startPosition) >= maxDistance)
                movingForward = !movingForward;  // Reverse direction
        }
        else if (moveVertical)
        {
            if (Mathf.Abs(transform.position.y - startPosition) >= maxDistance)
                movingForward = !movingForward;  // Reverse direction
        }
    }

    // Detect when the player is on the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.transform;  // Assign the player's transform
        }
    }

    // When the player leaves the platform, stop moving with it
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = null;  // Nullify the player reference
        }
    }
}