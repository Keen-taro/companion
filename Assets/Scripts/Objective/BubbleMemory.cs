using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMemory : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStateMachine>().PlayCollectedSound();
            BubbleMemoryController.singleton.CollectedMemory();

            Destroy(gameObject);
        }
    }
}
