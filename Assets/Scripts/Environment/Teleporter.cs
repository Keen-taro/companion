using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public int numTp;
    private float tpCooldownTime = 1f, tpReady;
    private bool playerInArea;
    private Transform playerPosition;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPosition = other.GetComponent<Transform>();
            playerInArea = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInArea = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInArea && Time.time >= tpReady)
        {
            if (numTp == 1)
            {
                BrokenSpaceController.singleton.TeleportToTwo(playerPosition);
            }
            else if (numTp == 2)
            {
                BrokenSpaceController.singleton.TeleportToOne(playerPosition);
            }

            tpReady = Time.time + tpCooldownTime;
        }
    }
}
