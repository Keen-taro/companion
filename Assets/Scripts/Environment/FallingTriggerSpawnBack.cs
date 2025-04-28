using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTriggerSpawnBack : MonoBehaviour
{
    [SerializeField] private Transform fixedSpawnFallingPosition;
    PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = fixedSpawnFallingPosition.position;

            player = collision.GetComponent<PlayerController>();

            player.Die();
        }
    }
}
