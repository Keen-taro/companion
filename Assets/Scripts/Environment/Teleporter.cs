using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform targetTeleporter; // Assign target teleporter langsung di Inspector
    private float tpCooldownTime = 1f, tpReady;
    private bool playerInArea;
    private Transform playerPosition;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPosition = other.transform;
            playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInArea = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInArea && Time.time >= tpReady && targetTeleporter != null)
        {
            playerPosition.position = targetTeleporter.position;
            tpReady = Time.time + tpCooldownTime;
        }
    }
}
