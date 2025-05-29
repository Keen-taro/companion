using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMemory : MonoBehaviour
{
    private bool playerInTheArea;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInTheArea)
        {
            BubbleMemoryController.singleton.CollectedMemory();
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTheArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerInTheArea = false;
    }
}
