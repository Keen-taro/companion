using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Transform out_gate;
    public float teleportCooldown = 5f;
    private static bool isTeleporting = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(TeleportWithCooldown(other.transform));
        }
    }


    private IEnumerator TeleportWithCooldown(Transform player)
    {
        // Set the flag to prevent instant re-teleporting
        isTeleporting = true;

        // Teleport the player to the target position
        player.position = out_gate.position;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(teleportCooldown);

        // Allow teleporting again after the cooldown
        isTeleporting = false;
    }
}
