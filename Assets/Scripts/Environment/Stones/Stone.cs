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

    private bool isPuzzleGridCompleted;
    public GameObject GridPuzzleGameUnlockWithThis;

    public Timer stoneTimer;
    public GameObject temporaryCloseTutorial;

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
            GridPuzzleGameUnlockWithThis.SetActive(false);
            //Debug.Log("Player left the trigger zone.");
        }
    }

    private void Awake()
    {
        gameObject.tag = "Stone";
        audioSource = GetComponent<AudioSource>();

        if (stoneTimer == null)
        {
            Debug.Log("Stone Timer not found");
        }
    }

    void Update()
    {
        // 1. Handle Input & Interaksi (Tetap di Update)
        if (isPlayerInRange && !isPickedUp && Input.GetKeyDown(KeyCode.F))
        {
            if (!isPuzzleGridCompleted)
            {
                temporaryCloseTutorial.SetActive(false);
                GridPuzzleGameUnlockWithThis.SetActive(true);
                GridPuzzleGameUnlockWithThis.GetComponent<Timer>().StartTimer();
                GridPuzzleGameUnlockWithThis.GetComponent<Timer>().attempCount++;
                return;
            }

            isPickedUp = true;
            stoneTimer.StartTimer();
            PlayPickupSound();
        }

        // 2. Reset Posisi Batu (Tetap di Update)
        if (isPickedUp && Input.GetKeyDown(KeyCode.L))
        {
            if (playerTransform == null) Debug.Log("Player Missing");
            transform.position = playerTransform.position + offset;
        }

        // 3. Drop Batu (Tetap di Update)
        if (Input.GetKeyDown(KeyCode.G) && isPickedUp)
        {
            Drop();
            PlayDropSound();
            isPickedUp = false;
        }
    }

    private void LateUpdate()
    {
        // 4. Update Posisi Batu ke Pemain (Pindah ke LateUpdate)
        if (isPickedUp)
        {
            transform.position = playerTransform.position + offset;
        }
    }

    public void FinishThePuzzle()
    {
        isPuzzleGridCompleted = true;
        GridPuzzleGameUnlockWithThis.SetActive(false);
    }

    public void Drop()
    {
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
