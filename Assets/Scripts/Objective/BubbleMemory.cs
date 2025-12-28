using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMemory : MonoBehaviour
{
    private bool playerInTheArea;
    public Vector3 rotationSpeed = new Vector3(0, 0, 50f);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInTheArea)
        {
            BubbleMemoryController.singleton.CollectedMemory();
            Destroy(gameObject);
        }

        transform.Rotate(rotationSpeed * Time.deltaTime);
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
