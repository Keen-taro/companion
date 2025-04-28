using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public string stoneElement;

    [SerializeField] private AudioClip pickupSound;  // Sound to play when the stone is picked up
    [SerializeField] private AudioClip dropSound;  // Sound to play when the stone is picked up
    private AudioSource audioSource;

    public bool isPickedUp = false; // Whether the stone is picked up
    public bool isPlayerInRange = false; // Whether the player is in the trigger zone
    private Transform playerTransform; // Player's transform to follow the player
    private Vector3 offset = new Vector3(0, 2f, 0); // Position offset

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerTransform = other.transform; // Store the player's transform
            //Debug.Log("Player entered the trigger zone.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerTransform = null;
            //Debug.Log("Player left the trigger zone.");
        }
    }

    private void Awake()
    {
        gameObject.tag = "Stone";
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Debug.Log("The stone have tag: " + gameObject.tag);
        //Debug.Log("The stone have name: " + gameObject.name);


        if (isPlayerInRange && !isPickedUp && Input.GetKeyDown(KeyCode.F))
        {
            isPickedUp = true;
            PlayPickupSound();
            //Debug.Log("Picking up the stone");
        }

        if (isPickedUp && playerTransform != null)
        {
            // Follow the player with an offset
            transform.position = playerTransform.position + offset;
        }

        
        if (Input.GetKeyDown(KeyCode.G) && isPickedUp)
        {
            // Drop the stone
            Drop();
            PlayDropSound();
            isPickedUp = false;
        }
        
    }

    public void Drop()
    {
        //Debug.Log("Stone dropped.");
        // Simply stop following the player and place it slightly below its current position
        transform.position = transform.position - offset;
    }

    private void PlayPickupSound()
    {
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }

    private void PlayDropSound()
    {
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(dropSound);
        }
    }
}
